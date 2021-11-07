import os
import cv2 as cv
import json

datasets_path = "C:\\Users\\felip\\AppData\\LocalLow\\DefaultCompany\\Drone - Arena 2021\\a23123b2-1208-4cc1-a10e-ea2111941bd9"

info_folder = datasets_path+"/"+"data"
alvo = "C:\\Users\\felip\\Desktop\\mostradores"
os.chdir(datasets_path)

for i in range(10):
    for p in range(10):
        for k in range(2):
            for l in range(2):
                for m in range(10):
                    os.makedirs("C:\\Users\\felip\\Desktop\\DEFINITIVO\\"+str(i)+"\\"+str(p)+"\\"+['+','-'][k]+"\\"+str(l)+"\\"+str(m))
