# -*- coding: utf8 -*-
from Jeu.Spline.spline import *
import re
motif = re.compile("(Spline\d+\.\S+)\s*=\s*([-+]?(\d+(\.\d*)?|\d*\.\d+)([eE][-+]?\d+)?).*")
motifsep = re.compile("~+.*")

def parse(fichier):
    file = open(fichier)
    lines = file.readlines()
    cursplines=[]
    curbeziers=[]
    curpoint=Point(0,0)
    curpoints=[]
    nbpoints=0
    numcoord=0
    first=True
    for line in lines:
        group = re.match(motif,line)
        if(group is not None):
            coord = float(group.group(2))
            if(numcoord % 2 == 0):
                curpoint.x=coord
            else:
                curpoint.y=coord
                curpoints.append(curpoint)
                curpoint=Point(0,0)
                nbpoints+=1
            numcoord+=1
            if(nbpoints == 6):
                if(first):
                    curbezier=Bezier([curpoints[0],curpoints[1],curpoints[3],curpoints[4]])
                else:
                    curbezier=Bezier([curpoints[0],curpoints[2],curpoints[3],curpoints[4]])
                first=False
                curbeziers.append(curbezier)
                curpoints=curpoints[3:]
                nbpoints=3
        group = re.match(motifsep,line)
        if(group is not None):
            curspline = Spline(curbeziers)
            cursplines.append(curspline)
            curbeziers=[]
    return cursplines

if __name__ == "__main__":
    sp = parse("C:\\Twinkle Star Sprites\\Python\\data\\Formation\\f1.txt")
    print(sp)