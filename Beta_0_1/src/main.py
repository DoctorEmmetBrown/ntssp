# -*- coding: utf8 -*-
from PyQt4.QtCore import *
from PathStroke import *
from FormationEditorWindow import *
from res import *
def main():
    app = QApplication([]);

    mainWindow = FormationEditorWindow();
    mainWindow.show();
    #pathStrokeWidget = PathStrokeWidget();
    #pathStrokeWidget.show();

    return app.exec_();

main()