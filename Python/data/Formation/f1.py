from Jeu.script import *
from Xml.handlers import *
class F1(FormationScript):
    script=None
    def __init__(self,perso,app,form):
        super(F1,self).__init__(form)
        self.perso = perso
        self.app = app
        self.formation.start(0.004,20,perso)
    def update(self):
        f=False
        if(self.formation.followSplines()):
            f=True
        self.formation.update()
        return f
form={}
scripts=[]
def newScript(app,perso):
    fs = F1(perso,app,form[(perso)])
    scripts.append(fs)
def update():
    finished=[]
    for s in scripts:
        if(s.update()):
            finished.append(s)
    for f in finished:
        scripts.remove(f)
    return finished
def load(app):
    global form
    f = getFormationFromXML("..\\data\\Formation\\f1.xml",app.jeu.stage)
    for p in app.jeu.persos:
        form[(p)]=f.clone()