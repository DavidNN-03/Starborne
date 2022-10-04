#what about actions/events, interfaces, enums, scriptable objects
#what about non-monobehaviours?

import os

types = ["void", "int", "float", "string", "Vector2", "Vector3", "Mesh", "Material", "Sprite" "Transform", "GameObject", "Rigidbody", "Collider"]
allPaths = []
pathsToDiagram = []

class ClassElement:
    def __init__(self, parentClass, interfaces, variables, functions):
        self.parentClass = parentClass
        self.interfaces = interfaces
        self.variables = variables
        self.functions = functions

        

def PrintDiagram(className, variables, functions):
    s = ""
    for i in range(5):
        print()
        s += "\n"
    
    print("=" * 40)
    s += "=" * 40
    s += "\n"
    print(className)
    s += className
    s += "\n"
    print("-" * 40)
    s += "-" * 40
    s += "\n"

    for i in variables:
        print(i)
        s += i
        s += "\n"
    
    for i in range(1):
        print()

    print("-" * 40)
    s += "-" * 40
    s += "\n"

    for i in functions:
        print(i)
        s += i
        s += "\n"
    
    print("=" * 40)
    s += "=" * 40
    s += "\n"

    return s

def FindAllTypes(paths):
    for path in paths:
            file = open(path)
            words = file.read().split()

            for i in range(len(words)):
                if words[i] == "class":
                    types.append(words[i+1])

def FindAllPaths(path):
    files = os.listdir(path)
    folders = []

    for file in files:
        if file[-4:] == "meta":
            files.remove(file)
    
    for file in files:
        if not '.' in file:
            folders.append(file)
        else:
            allPaths.append(path + "/" + file)
    
    for folder in folders:
        FindAllPaths(path + "/" + folder)

def FindAllPathsToDiagram(path):
    files = os.listdir(path)
    folders = []

    for file in files:
        if file[-4:] == "meta":
            files.remove(file)
    
    for file in files:
        if not '.' in file:
            folders.append(file)
        else:
            pathsToDiagram.append(path + "/" + file)
    
    for folder in folders:
        FindAllPathsToDiagram(path + "/" + folder)

def GetIndexOfWord(word, arr, exceptionChar):
    for i in range(len(arr)):
        if not arr[i].find(word) == -1 and arr[i].find(exceptionChar) == -1:
            return i
    return -1

def CreateInterfaceELement(words):
    interfaceName = ""
    for i in range(len(words)):
        if words[i] == "interterface":
            interfaceName = words[i+1]
            break

def CreateClassElement(words):
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

    if startIndex == -1 or endIndex == -1:
        print("ERROR INDEX IS -1")
    else:
        variablesSection = words[startIndex:endIndex]

    variablesSection = words[startIndex:endIndex]

    className = ""
    functions = []
    variables = []

    for i in range(len(words)):
        if words[i] == "class":
            startIndex = i
            endIndex = -1

            for j in range(i,len(words)):
                if '{' in words[j]:
                    endIndex = j
                    break
            
            for i in words[startIndex:endIndex]:
                className += i + " "

    if className == "":
        return

    for i in range(1, len(variablesSection)-1):
        if variablesSection[i] in types and not variablesSection[i-1] == "class":
            variables.append(variablesSection[i] + " " + variablesSection[i+1])
        elif variablesSection[i][:-2] in types and variablesSection[i][-2:] == "[]":
            variables.append(variablesSection[i] + " " + variablesSection[i+1])

    for i in range(len(words)):
        if words[i] in types:
            isFunction = '(' in words[i+1]
            if isFunction:
                s = words[i] + " " + words[i+1]
                if not ')' in words[i+1]: 
                    for j in range(i+2, len(words)):
                         s += " " + words[j]
                         if ')' in words[j]:
                            break
                functions.append(s)

    return PrintDiagram(className, variables, functions)

def CreateElement(path):
    file = open(path)
    words = file.read().split()

    if "class" in words:
        return CreateClassElement(words)
    #elif "interface" in words:
        #return CreateInterfaceELement(words)
    else:
        return ""


def CreateClassDiagram(typesFolder, foldersToDiagram):
    FindAllPaths(typesFolder)
    FindAllPathsToDiagram(foldersToDiagram)
    FindAllTypes(allPaths)
    
    fileString = ""

    for i in pathsToDiagram:
        fileString += CreateElement(i)
    
    file = open("./Documentation/ClassDiagram.txt", "w")
    file.write(fileString)
    file.close()

CreateClassDiagram("Assets/Scripts", "Assets/Scripts")
