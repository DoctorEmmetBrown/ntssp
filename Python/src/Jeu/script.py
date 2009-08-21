# -*- coding: utf8 -*-
class Script(object):
    def __init__(self,sprites,sons):
        self.sprites=sprites
        self.sons=sons
    #a redefinir dans le script
    def update(self):
        pass
class FormationScript(object):
    def __init__(self,formation):
        self.formation=formation
    def update(self):
        pass 

def loadScript(fichier):
    glob = {}
    exec(compile(open(fichier).read(), fichier, 'exec'),glob)
    return glob