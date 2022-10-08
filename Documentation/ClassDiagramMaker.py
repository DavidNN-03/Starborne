#what about actions/events, dictionaries, 

#names of all variable types must be added to 'types', 
#this does not include types defined in the files that are being documented

#This program supports arrays and lists

#each file can only include 1 class, enum, or interface declaration
#multiple declarations will result in missing documentation


#sites are created before all SiteObjects are created

classObjects = []
interfaceObjects = []
enumObjects = []

import os

projectName = "Starborne"

types = ["void", "bool", "int", "float", "string", "Vector2", "Vector3", "Mesh", "Material", "Sprite", "Transform", "RectTransform", "GameObject", "Rigidbody", "Collider", "MonoBehaviour", "ScriptableObject", "UnityEvent", "IEnumerator", "Ray", "TextMeshProUGUI"]

allInterfaces = []
allNamespaces = []

allPaths = [] #array of paths to every file

class FunctionObject:
    def __init__(self, returnType, functionName, description):
        self.returnType = returnType
        self.functionName = functionName
        self.description = description

class VariableObject:
    def __init__(self, variableType, variableName, description):
        self.variableType = variableType
        self.variableName = variableName
        self.description = description

class ClassElement:
    def __init__(self, className, namespace, parentClass, interfaces, description, variables, functions):
        self.className = className
        self.namespace = namespace
        self.parentClass = parentClass
        self.interfaces = interfaces
        self.variables = variables
        self.functions = functions
        self.description = description

class InterfaceElement:
    def __init__(self, interfaceName, description):
        self.interfaceName = interfaceName
        self.description = description

class EnumObject:
    def __init__(self, enumName, options, description):
        self.enumName = enumName
        self.options = options
        self.description = description

class EnumOption:
    def __init__(self, optionName, description):
        self.optionName = optionName
        self.description = description

def AddAllChildFilesToArray(path, arr):
    files = os.listdir(path)
    folders = []

    for file in files:
        if file[-4:] == "meta":
            files.remove(file)
    
    for file in files:
        if not '.' in file:
            folders.append(file)
        elif file[-3:] == ".cs":
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
        if not arr[i].find(word) == -1 and (arr[i].find(exceptionChar) == -1 or exceptionChar == ""):
            return i
    return -1

def CreateTag(tagType, argumentsText, inbetweenText):
    tag = "<" + tagType + " " + argumentsText + ">" + inbetweenText + "</" + tagType + ">"
    return tag

def CreateSidebar():
    sideBarTags = CreateTag("h2", "", "Documentation")
    sideBarTags += "<br>" * 2
    sideBarTags += CreateTag("h3", "", "Classes")

    for c in classObjects:
        sitePath = "./" + c.className + ".html"
        sideBarTags += CreateTag("a", "href=\"" + sitePath + "\"", c.className)
        sideBarTags += "<br>" * 2

    sideBarTags += "<br>"
    sideBarTags += CreateTag("h3", "", "Interfaces")

    for i in interfaceObjects:
        sitePath = "./" + i.interfaceName + ".html"
        sideBarTags += CreateTag("a", "href=\"" + sitePath + "\"", i.interfaceName)
        sideBarTags += "<br>" * 2

    sideBarTags += "<br>"
    sideBarTags += CreateTag("h3", "", "Enums")

    for i in enumObjects:
        sitePath = "./" + i.enumName + ".html"
        sideBarTags += CreateTag("a", "href=\"" + sitePath + "\"", i.enumName)
        sideBarTags += "<br>" * 2
    
    return sideBarTags

def CreateClassSite(classObject):
    s = open("./Documentation/Templates/ClassSiteTemplate.html").read()

    #Add text in <head>
    #----------------------------------------------------------------------------------

    #add title
    pageTitle = projectName + " - " + classObject.className
    splitIndex = s.find("<title>") + 7
    s = s[:splitIndex] + pageTitle + s[splitIndex:]

    #----------------------------------------------------------------------------------
    #Add text in topnav div
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"topnav\"") + 15

    s = s[:splitIndex] + CreateTag("a", "href=\"./index.html\"", "Starborne") + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text and links in sidebar
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"sidebar\"") + 16
              
    sideBarTags = CreateSidebar()

    s = s[:splitIndex] + sideBarTags + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text in main-text-editor div
    #----------------------------------------------------------------------------------

    #add header
    pageHeader = classObject.className
    splitIndex = s.find("main-text-container") + 21
    mainDivText = CreateTag("h1", "", pageHeader)
    mainDivText += "<br>" * 1
    
    #add namespace
    if not classObject.namespace == None and not classObject.namespace == "":
        mainDivText += CreateTag("p", "", "Namespace: " + classObject.namespace)

    #add parent-class
    if not classObject.parentClass == None and not classObject.parentClass == "":
        mainDivText += CreateTag("p", "", "Inherits from: " + classObject.parentClass)
    #print(classObject.className + " inherits from " + classObject.parentClass)
    
    #add implemented interfaces
    if not classObject.interfaces == None and len(classObject.interfaces) > 0:
        interfacesText = ""
        for i in range(len(classObject.interfaces)):
            interfacesText += classObject.interfaces[i]
            if i < len(classObject.interfaces) - 1:
                interfacesText += ", "

        mainDivText += CreateTag("p", "", "Implements interfaces: " + interfacesText)

    mainDivText += "<br>" + classObject.description + "<br>" * 2

    #create variables table
    if not classObject.variables == None and len(classObject.variables) > 0:
        tableText = ""

        for i in range(len(classObject.variables)):
            tableRowText = CreateTag("td", "", classObject.variables[i].variableType) 
            tableRowText += CreateTag("td", "",  classObject.variables[i].variableName)
            tableRowText += CreateTag("td", "",  classObject.variables[i].description)
                        
            tableRowText = "<tr>" + tableRowText + "</tr>"

            tableText += tableRowText

        tableText = CreateTag("table", "class=\"myTable\"", tableText)
        mainDivText += tableText
    
    mainDivText += "<br>" * 2

    #create functions table
    if not classObject.functions == None and len(classObject.functions) > 0:
        tableText = ""

        for i in range(len(classObject.functions)):
            tableRowText = CreateTag("td", "", classObject.functions[i].returnType) 
            tableRowText += CreateTag("td", "", classObject.functions[i].functionName)
            tableRowText += CreateTag("td", "", classObject.functions[i].description)

            tableRowText = "<tr>" + tableRowText + "</tr>"

            tableText += tableRowText

        tableText = CreateTag("table", "class=\"myTable\"", tableText)
        mainDivText += tableText

    #concatinate strings
    s = s[:splitIndex] + mainDivText + "<br>" * 5 + s[splitIndex:]
    
    #----------------------------------------------------------------------------------
    #save file
    #----------------------------------------------------------------------------------

    path = "./Documentation/HTML/" + classObject.className + ".html"
    file = open(path, "w")
    file.write(s)
    file.close()

def CreateInterfaceSite(interfaceObject):
    s = open("./Documentation/Templates/ClassSiteTemplate.html").read()

    #Add text in <head>
    #----------------------------------------------------------------------------------

    #add title
    pageTitle = projectName + " - " + interfaceObject.interfaceName
    splitIndex = s.find("<title>") + 7
    s = s[:splitIndex] + pageTitle + s[splitIndex:]

    
    #----------------------------------------------------------------------------------
    #Add text in topnav div
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"topnav\"") + 15

    s = s[:splitIndex] + CreateTag("a", "href=\"./index.html\"", "Starborne") + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text and links in sidebar
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"sidebar\"") + 16
    
    sideBarTags = CreateSidebar()

    s = s[:splitIndex] + sideBarTags + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text in main-text-editor div
    #----------------------------------------------------------------------------------

    #add header
    pageHeader = interfaceObject.interfaceName
    splitIndex = s.find("main-text-container") + 21
    mainDivText = CreateTag("h1", "", pageHeader)
    mainDivText += "<br>" * 1
    
    mainDivText += "<br>" + interfaceObject.description + "<br>" * 2

    #concatinate strings
    s = s[:splitIndex] + mainDivText + "<br>" * 5 + s[splitIndex:]
    
    #----------------------------------------------------------------------------------
    #save file
    #----------------------------------------------------------------------------------

    path = "./Documentation/HTML/" + interfaceObject.interfaceName + ".html"
    file = open(path, "w")
    file.write(s)
    file.close()

def CreateEnumSite(enumObject):
    s = open("./Documentation/Templates/ClassSiteTemplate.html").read()

    #Add text in <head>
    #----------------------------------------------------------------------------------

    #add title
    pageTitle = projectName + " - " + enumObject.enumName
    splitIndex = s.find("<title>") + 7
    s = s[:splitIndex] + pageTitle + s[splitIndex:]

    
    #----------------------------------------------------------------------------------
    #Add text in topnav div
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"topnav\"") + 15

    s = s[:splitIndex] + CreateTag("a", "href=\"./index.html\"", "Starborne") + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text and links in sidebar
    #----------------------------------------------------------------------------------
    splitIndex = s.find("class=\"sidebar\"") + 16
    
    sideBarTags = CreateSidebar()

    s = s[:splitIndex] + sideBarTags + s[splitIndex:]
    #----------------------------------------------------------------------------------
    #Add text in main-text-editor div
    #----------------------------------------------------------------------------------

    #add header
    pageHeader = enumObject.enumName
    splitIndex = s.find("main-text-container") + 21
    mainDivText = CreateTag("h1", "", pageHeader)
    mainDivText += "<br>" * 1
    
    mainDivText += "<br>" + enumObject.description + "<br>" * 2

    #concatinate strings
    s = s[:splitIndex] + mainDivText + "<br>" * 5 + s[splitIndex:]
    
    #----------------------------------------------------------------------------------
    #save file
    #----------------------------------------------------------------------------------

    path = "./Documentation/HTML/" + enumObject.enumName + ".html"
    file = open(path, "w")
    file.write(s)
    file.close()

def CreateIndexSite():
    s = open("./Documentation/Templates/ClassSiteTemplate.html").read()

    path = "./Documentation/HTML/index.html"
    file = open(path, "w")
    file.write(s)
    file.close()

def FindComment(index , section, commentAfterString):
    comment = ""
    startIndex = -1

    for i in range(index, len(section)):
        if commentAfterString in section[i]:
            startIndex = i+1
            break
    
    if startIndex < len(section) and "/*" in section[startIndex]:
        comment = section[startIndex][2:]
        if i+3 < len(section):
            for j in range(startIndex+1, len(section)):
                if "*/" in section[j]:
                    comment += " " + section[j][:-2]
                    break
                comment += " " + section[j]
    else:
        comment = "No description found"
    
    return comment

def CreateClassObject(words):
    #variables should be found between the class declaration and the first function
    endWord = '(' #should be found at the end of the first function
    startIndex = -1
    endIndex = -1
    
    #Ignore variables with tags such as [Header()] and [Tooltip()]
    endIndex = GetIndexOfWord(endWord, words, '[')

    for i in range(GetIndexOfWord('class', words, ""), len(words)):
        if '{' in words[i]:
            startIndex = i
            break

    if endIndex == -1:
        endIndex = len(words)-1 

    variablesSection = words[startIndex:endIndex]

    className = ""
    namespace = ""
    parentClass = ""
    interfaces = []
    description = ""
    functions = []
    variables = []

    #find class name, namespace, parent class, and interfaces
    for i in range(len(words)-1):
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
                    word = word[:-1] #remove the comma
                
                if word in types: #add parent class
                    parentClass = word
                elif word in allInterfaces: #add interfaces
                    interfaces.append(word)
            break
 
    #check class has name
    if className == "":
        return

    #find class description
    if len(interfaces) > 0:
        description = FindComment(0, words, interfaces[len(interfaces)-1])
    elif not parentClass == "":
        description = FindComment(0, words, parentClass)
    else:
        description = FindComment(0, words, className)

    for i in range(1, len(variablesSection)-1): #find variables
        if variablesSection[i] in types and not variablesSection[i-1] == "class": #Basic variables
            variableType = variablesSection[i]
            variableName = variablesSection[i+1]
            variableDescription = ""

            if variableName[-1:] == ';':
                variableName = variableName[:-1] #remove the semi-colon

            variableDescription = FindComment(i, variablesSection, ';')

            newVariable = VariableObject(variableType, variableName, variableDescription)
            variables.append(newVariable)

        elif variablesSection[i][:-2] in types and variablesSection[i][-2:] == "[]": #Arrays
            variableName = variablesSection[i+1]

            if variableName[-1:] == ';':
                variableName = variableName[:-1] #remove the semi-colon

            variableDescription = FindComment(i, variablesSection, ';')

            newVariable = VariableObject(variablesSection[i], variableName, variableDescription)
            variables.append(newVariable)
        
        elif variablesSection[i][0:5] == "List<":
            variableName = variablesSection[i+1]

            variableDescription = FindComment(i, variablesSection, ';')

            newVariable = VariableObject(variablesSection[i], variableName, variableDescription)
            variables.append(newVariable)

    for i in range(len(words)): #find functions
        if not words[i] in types:
            continue
        isFunction = '(' in words[i+1]
        if not isFunction:
            continue
        
        returnType = words[i]
        functionName = words[i+1]
        comment = ""

        if not ')' in words[i+1]: 
            for j in range(i+2, len(words)):
                 functionName += " " + words[j]
                 if ')' in words[j]:
                    break

        comment = FindComment(i, words, ')')

        functionObject = FunctionObject(returnType, functionName, comment)

        functions.append(functionObject)

    classObject = ClassElement(className, namespace, parentClass, interfaces, description, variables, functions)
    return classObject

def CreateInterfaceObject(words):
    interfaceName = ""
    description = ""

    for i in range(len(words)):
        if words[i] == "interface":
            interfaceName = words[i+1]
            break
    
    description = FindComment(0, words, interfaceName)

    interfaceObject = InterfaceElement(interfaceName, description)
    return interfaceObject

def CreateEnumObject(words):
    enumName = ""
    options = []
    description = ""

    startIndex = GetIndexOfWord('{', words, "") + 1
    endIndex = GetIndexOfWord('}', words, "")

    optionsSection = words[startIndex:endIndex]

    #find enumName
    for i in range(len(words)):
        if words[i] == "enum":
            enumName = words[i+1]
            break

    #find enum options
    for i in range(len(optionsSection)):
        optionName = ""
        optionDescription = ""

        if optionsSection[i][-1] == ',':
            optionName = optionsSection[i][:-1]
        else:
            optionName = optionsSection[i]
        
        #find option comment

        option = EnumOption(enumName, description)
        options.append(option)

    description = FindComment(0, words, enumName)

    enumObject = EnumObject(enumName, options, description)
    return enumObject

def SortClassObjects(arr):
    if len(arr) > 1:
        mid = len(arr)//2
        L = arr[:mid]
        R = arr[mid:]

        SortClassObjects(L)
        SortClassObjects(R)

        i = j = k = 0

        while i < len(L) and j < len(R):
            if L[i].className < R[j].className:
                arr[k] = L[i]
                i += 1
            else:
                arr[k] = R[j]
                j = j + 1 
            k += 1

        while i < len(L):
            arr[k] = L[i]
            i += 1
            k += 1
  
        while j < len(R):
            arr[k] = R[j]
            j += 1
            k += 1

def SortInterfaceObjects(arr):
    if len(arr) > 1:
        mid = len(arr)//2
        L = arr[:mid]
        R = arr[mid:]

        SortClassObjects(L)
        SortClassObjects(R)

        i = j = k = 0

        while i < len(L) and j < len(R):
            if L[i].interfaceName < R[j].interfaceName:
                arr[k] = L[i]
                i += 1
            else:
                arr[k] = R[j]
                j = j + 1 
            k += 1

        while i < len(L):
            arr[k] = L[i]
            i += 1
            k += 1
  
        while j < len(R):
            arr[k] = R[j]
            j += 1
            k += 1

def CreateClassDiagram(rootFolderPath):
    #Find all files and types
    AddAllChildFilesToArray(rootFolderPath, allPaths) #adds all files under the given folder to the allPaths array
    FindAllTypes(allPaths) #adds all classes/types to the types array

    #Add data from classes and interfaces to objects in classObjects and interfaceObjects
    for filePath in allPaths:
        file = open(filePath)
        words = file.read().split()  

        if "class" in words:
            classObject = CreateClassObject(words)
            classObjects.append(classObject)
        elif "interface" in words:
            interfaceObject = CreateInterfaceObject(words)
            interfaceObjects.append(interfaceObject)
        elif "enum" in words:
            enumObject = CreateEnumObject(words)
            enumObjects.append(enumObject)
    
    SortClassObjects(classObjects)
    SortInterfaceObjects(interfaceObjects)

    for classObject in classObjects:
        CreateClassSite(classObject)

    for interfaceObject in interfaceObjects:
        CreateInterfaceSite(interfaceObject)

    for enumObject in enumObjects:
        CreateEnumSite(enumObject)

    for namespace in allNamespaces:
        print()

    CreateIndexSite()

CreateClassDiagram("Assets/Scripts")