import cv2 as cv
import json

import os

#mude o datasets_path e alvo para se adequar à sua situação
datasets_path = "C:\\Users\\felip\\AppData\\LocalLow\\DefaultCompany\\Drone - Arena 2021\\96bbc25e-2045-407a-a152-c425b058a9e9"

contador = 0
print("to vivo")
info_folder = datasets_path+"/"+"data"
alvo = "C:\\Users\\felip\\Desktop\\mostradores"
os.chdir(datasets_path)


for i in range(13):
    
    if (i < 10):
        fl = open(info_folder+"/captures_00"+str(i)+".json")
    else:
        fl = open(info_folder+"/captures_0"+str(i)+".json")
    captures = json.load(fl)
    fl.close()
    for capture in captures["captures"]: # Itera nas imagens
        empty = True
        if (len(capture["annotations"][0]["values"]) != 0) and (len(capture["annotations"][0]["values"][0]["label_name"])>9):
            img_file_name = datasets_path+"/"+capture["filename"]
            file = capture["annotations"][0]["values"][0]["label_name"] 
            if capture["annotations"][0]["values"][0]["label_name"][0] == "M":
                if((len(file)<10)): #algumas imagens dão problemas, o que pode ser consertado por adicionar img_file_name[-8:] == "_170.png" no if e semelhantemente para cada imagem que der errado (a ultima imagem no print)
                    continue
                print(img_file_name) 
                imagem = cv.imread(img_file_name)
                imagem = cv.cvtColor(img_file_name, cv.COLOR_BGR2GRAY)
                #print(imagem[(int(capture["annotations"][0]["values"][0]["y"]) + int(capture["annotations"][0]["values"][0]["height"]))][int(capture["annotations"][0]["values"][0]["x"])+13])
                imagem_cortada = imagem[(int(capture["annotations"][0]["values"][0]["y"])):((int(capture["annotations"][0]["values"][0]["y"]))+(int(capture["annotations"][0]["values"][0]["height"]))) , (int(capture["annotations"][0]["values"][0]["x"])):((int(capture["annotations"][0]["values"][0]["x"]))+(int(capture["annotations"][0]["values"][0]["width"])))]
                print(imagem_cortada.shape())
                if len(file) == 16:
                    digito1 = file[10]
                    digito2 = file[11]
                    sinal = file[13]
                    digito3 = file[14]
                    digito4 = file[15]

                elif len(file) == 15:
                    digito1 = file[10]
                    digito2 = file[11]
                    if file[13].isnumeric():
                        sinal = '+'
                        digito3 = file[13]
                        digito4 = file[14]
                    else:
                        sinal = file[13]
                        digito3 = '0'
                        digito4 = file[14]
                elif len(file) == 14:
                    digito1 = file[10]
                    digito2 = file[11]
                    sinal = '+'
                    digito3 = '0'
                    digito4 = file[13]
                resized = cv.resize(imagem_cortada, [100, 100], interpolation= cv.INTER_CUBIC)
                cv.imwrite("C:\\Users\\felip\\Desktop\\DEFINITIVO\\"+digito1+"\\"+digito2+"\\"+sinal+"\\"+digito3+"\\"+digito4+"\\l"+str(capture["filename"][-8:]), resized)
        
            
            
                
            