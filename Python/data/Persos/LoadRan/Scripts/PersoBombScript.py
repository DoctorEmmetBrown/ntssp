from Jeu.script import *
from Xml.handlers import *
t=[25, 25, 25, 25, -25, -25, -25, -25, -25, 25];
class PersoBombScript(Script):
    script=None
    def __init__(self, t, perso, app):
        super(PersoBombScript, self).__init__(t[0], t[1])
        self.perso = perso
        self.started=False
        self.first=True
        self.app = app
        #self.sprites[0].setSpeed(10)
        self.__fps = 0
        self.__it = 0
    def update(self):
       if(self.first):
           self.perso.block()
           self.x = self.perso.x
           self.y = self.perso.y
           self.x -= (self.sprites[0].display_rect.width - self.perso.currentSprite().display_rect.width)/2
           self.y -= (self.sprites[0].display_rect.height - self.perso.currentSprite().display_rect.height)/2 
           self.__fps=1
           self.__it=0
           self.first=False
       self.__fps+=1
       self.sprites[0].setCoord(self.x, self.y)
       self.sprites[0].update()
       if(self.sprites[0].changed):
           self.__fps=0
           self.y -= t[self.__it]
           self.__it+=1
       self.started=not self.sprites[0].end()
       if(not self.started):
           self.perso.unblock()
    def start(self):
        if(not self.started):
            self.first=True
            self.started=True
    @classmethod
    def getSingleton(cls, app, perso):
       if(cls.script is None):
           sc = getScriptFromXML("..\data\Persos\LoadRan\Scripts\PersoBombScript.xml", app.display.backbuffer)
           bs = PersoBombScript(sc, perso, app)
           cls.script=bs
       return cls.script

def newScript(app, perso):
    pbs=PersoBombScript.getSingleton(app, perso)
    pbs.start()
def update():
    if(PersoBombScript.script is not None and PersoBombScript.script.started):
        PersoBombScript.script.update()
def load(app, perso):
    PersoBombScript.getSingleton(app, perso)