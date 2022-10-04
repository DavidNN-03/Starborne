#what about actions/events, interfaces, enums, scriptable objects

from email import header
import os

projectName = "Starborne"

types = ["void", "int", "float", "string", "Vector2", "Vector3", "Mesh", "Material", "Sprite" "Transform", "GameObject", "Rigidbody", "Collider", "MonoBehaviour"]
allInterfaces = []
allNamespaces = []
allPaths = []

class ClassElement:
    def __init__(self, className, namespace, parentClass, interfaces, variables, functions):
        self.className = className
        self.namespace = namespace
        self.parentClass = parentClass
        self.interfaces = interfaces
        self.variables = variables
        self.functions = functions

class InterfaceElement:
    def __init__(self, interfaceName):
        self.interfaceName = interfaceName

def AddAllChildFilesToArray(path, arr):
    files = os.listdir(path)
    folders = []

    for file in files:
        if file[-4:] == "meta":
            files.remove(file)
    
    for file in files:
        if not '.' in file:
            folders.append(file)
        else:
            arr.append(path + "/" + file)
    
    for folder in folders:
        AddAllChildFilesToArray(path + "/" + folder, arr)

def FindAllTypes(paths):
    for path in paths:
            file = open(path)
            words = file.read().split()

            for i in range(len(words)):
                if words[i] == "namespace" and not words[i+1] in allNamespaces:
                    allNamespaces.append(words[i+1])

                if words[i] == "class":
                    types.append(words[i+1])
                elif words[i] == "interface":
                    allInterfaces.append(words[i+1])

def GetIndexOfWord(word, arr, exceptionChar):
    for i in range(len(arr)):
        if not arr[i].find(word) == -1 and arr[i].find(exceptionChar) == -1:
            return i
    return -1

def CreateTag(tagType, argumentsText, inbetweenText):
    tag = "<" + tagType + argumentsText + ">" + inbetweenText + "</" + tagType + ">"
    return tag

def CreateClassSite(classObject):
    s = open("./Documentation/Templates/ClassSiteTemplate.html").read()

    #add title
    pageTitle = projectName + " - " + classObject.className
    splitIndex = s.find("<title>") + 7
    s = s[:splitIndex] + pageTitle + s[splitIndex:]

    #add header, parent class, and inheritance
    pageHeader = classObject.className
    splitIndex = s.find("main-text-container") + 21
    text = CreateTag("h1", "", pageHeader)
    text += "<br>" * 3
    
    if not classObject.parentClass == None and not classObject.parentClass == "":
        text += CreateTag("p", "Inherits from: " + classObject.parentClass)
        print("class: " + classObject.className)
        print("parentClass: " + classObject.parentClass)

    if not classObject.interfaces == None and len(classObject.interfaces) > 0:
        interfacesText = ""
        for i in range(len(classObject.interfaces)):
            interfacesText += classObject.interfaces[i]
            if i < len(classObject.interfaces) - 1:
                interfacesText += ", "

        text += CreateTag("p", "", "Implements interfaces: " + interfacesText)

    s = s[:splitIndex] + text + "<br>" * 5 + s[splitIndex:]

    #create

    path = "./Documentation/HTML/" + classObject.className + ".html"
    file = open(path, "w")
    file.write(s)
    file.close()

def CreateInterfaceSite(interfaceObject):
    print()

def CreateInterfaceObject(words):
    interfaceName = ""
    for i in range(len(words)):
        if words[i] == "interterface":
            interfaceName = words[i+1]
            break
    
    interfaceObject = InterfaceElement(interfaceName)
    return interfaceObject

def CreateClassObject(words):
    #variables should be found between the class declaration and the first function
    startWord = 'class'
    endWord = '(' #should be found at the end of the first function
    startIndex = -1
    endIndex = -1
    
    #Ignore variables with tags such as [Header()] and [Tooltip()]
    startIndex = GetIndexOfWord(startWord, words, '[')
    endIndex = GetIndexOfWord(endWord, words, '[')

    if endIndex == -1:
        endIndex = len(words)-1 

    variablesSection = words[startIndex:endIndex]

    className = ""
    namespace = ""
    parentClass = ""
    interfaces = []
    functions = []
    variables = []

    for i in range(len(words)-1): #find class name, namespace, parent class, and interfaces
        if words[i] == "namespace" and namespace == "":
            namespace = words[i+1]
        elif words[i] == "class" and className == "":
            className = words[i+1]
        
            if not words[i+2] == ":":
                break
            
            for j in range(i+3,len(words)-1):
                word = words[j]

                if '{' in word:
                    break

                if word[-1:] == ',':
                    word = word[0:len(word)-2] #remove the comma
                
                if word in types: #add parent class
                    parentClass = word
                elif word in allInterfaces: #add interfaces
                    interfaces.append(word)
            break

    if className == "": #check class has name
        return

    for i in range(1, len(variablesSection)-1): #find variables
        if variablesSection[i] in types and not variablesSection[i-1] == "class": #Basic variables
            variables.append(variablesSection[i] + " " + variablesSection[i+1])
        elif variablesSection[i][:-2] in types and variablesSection[i][-2:] == "[]": #Arrays
            variables.append(variablesSection[i] + " " + variablesSection[i+1])

    for i in range(len(words)): #find functions
        if not words[i] in types:
            continue
        isFunction = '(' in words[i+1]
        if not isFunction:
            continue

        s = words[i] + " " + words[i+1]
        if not ')' in words[i+1]: 
            for j in range(i+2, len(words)):
                 s += " " + words[j]
                 if ')' in words[j]:
                    break
        functions.append(s)

    classObject = ClassElement(className, namespace, parentClass, interfaces, variables, functions)
    return classObject

def CreateClassDiagram(rootFolderPath):
    AddAllChildFilesToArray(rootFolderPath, allPaths) #adds all files under the given folder to the allPaths array
    FindAllTypes(allPaths) #adds all classes/types to the types array
    
    for filePath in allPaths:
        
        file = open(filePath)
        words = file.read().split()

        if "class" in words:
            classObject = CreateClassObject(words)
            CreateClassSite(classObject)
        elif "interface" in words:
            interfaceObject = CreateInterfaceObject(words)
            CreateInterfaceSite(interfaceObject)
    
    for namespace in allNamespaces:
        print()

    #create front-page index.html
    """
    filePath = allPaths[0]
    file = open(filePath)
    words = file.read().split()

    if "class" in words:
        classObject = CreateClassObject(words)
        CreateClassSite(classObject)
    elif "interface" in words:
        interfaceObject = CreateInterfaceObject(words)
        CreateInterfaceSite(interfaceObject)
    """

CreateClassDiagram("Assets/Scripts")
