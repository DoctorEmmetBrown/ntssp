from Bezier import *;
class EffectType:
    Circle = 0;
    Switch = 1;
    Rotate = 2;
    Arc = 3;
    Zero = 4;

MAX_POINTS = 10000;
MAX_ENEMIES_IN_FORMATION=20;
OUT_SCREEN = (-500,-500);
class EnemyPath:
    PathID = 0;
    def __init__(self):
        self.id = EnemyPath.PathID;
        EnemyPath.PathID = EnemyPath.PathID + 1;
        self.BezierSpline = BezierSpline();
        self.__lastPoint=Point(400,400);
        self.AddPoint(Point(400,300));
    def AddPoint(self,point):
        p1 = self.__lastPoint;
        p4 = point;
        vx = p4.x - p1.x;
        vy = p4.y - p1.y;
        p2 = Point(p1.x + vx * 1.0/3.0,p1.y + vy * 1.0/3.0);
        p3 = Point(p1.x + vx * 2.0/3.0,p1.y + vy * 2.0/3.0);
        self.BezierSpline.addCurve(CubicBezier(p1,p2,p3,p4));
        self.__lastPoint = p4;
    #num (enemy number in the formation )
    def getNextPoint(self,num):
        next = OUT_SCREEN;
        if(num <= self.__lastEnemy):
            next = self.BezierSpline.getPoint(self.__t,self.__max);
            self.__t = self.__t + 1;
        self.__interTime = self.__interTime + 1;
        if(self.__interTime == self.timeBeetweenEnemies):
            self.__lastEnemy = max(self.__lastEnemy + 1,MAX_ENEMIES_IN_FORMATION);
        return next;


class CirclePath(EnemyPath):
    def __init__(self):
        return;

class SwitchPath(EnemyPath):
    def __init__(self):
        return;

class RotatePath(EnemyPath):
    def __init__(self):
        return;
    
class ArcPath(EnemyPath):
    def __init__(self):
        return;