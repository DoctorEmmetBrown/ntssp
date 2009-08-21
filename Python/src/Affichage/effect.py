# -*- coding: utf8 -*-
class Effect:
    def __init__(self,surfaces):
        self.surfaces = surfaces
        self.origpalettes = [surf.get_palette() for surf in surfaces]
        self.curpalettes = [surf.get_palette() for surf in surfaces]
    def setPalette(self,index,pal):
        self.surfaces[index].set_palette(pal)
        self.curpalettes[index] = pal
    def getOrigPalette(self,index):
        return self.origpalettes[index]
    def getCurPalette(self,index):
        return self.curpalettes[index]
    def resetAll(self):
        i=0
        for surf in self.surfaces:
            surf.set_palette(self.origpalettes[i])
            i+=1
        self.curpalettes = [pal for pal in self.origpalettes]
    def reset(self,index):
        self.surfaces[index].set_palette(self.origpalettes[index])
        self.curpalettes[index] = self.origpalettes[index]
        
class DefaultEffects(Effect):
    def __init__(self,surfaces):
        Effect.__init__(self,surfaces)
        self.coloreffects={}
        self.brighteffects={}
    def setColorEffectAll(self,maskr,maskg,maskb):
        for i in range(len(self.surfaces)):
            self.setColorEffect(i, maskr, maskg, maskb)
    def setColorEffect(self,index,maskr,maskg,maskb):
        if((index,maskr,maskg,maskb) in self.coloreffects):
            newpal=self.coloreffects[(index,maskr,maskg,maskb)]
        else:
            newpal =[(color[0] & maskr,color[1] & maskg, color[2] & maskb) for color in self.curpalettes[index]]
            self.coloreffects[(index,maskr,maskg,maskb)] = newpal
        self.setPalette(index, newpal)
    def setBrightEffectAll(self,r,g,b):
        for i in range(len(self.surfaces)):
            self.setBrightEffect(i,r,g,b)
    def setBrightEffect(self,index,r,g,b):
        if((index,r,g,b) in self.brighteffects):
            newpal=self.brighteffects[(index,r,g,b)]
        else:
            newpal=[(min(color[0]+r,255),min(color[1]+g,255),min(color[2]+b,255)) for color in self.curpalettes[index]]
            self.brighteffects[(index,r,g,b)] = newpal
        self.setPalette(index,newpal)