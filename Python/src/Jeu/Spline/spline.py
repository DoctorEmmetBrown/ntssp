# -*- coding: utf8 -*-
import math
# balf = Bezier Arc Length Function
balfax,balfbx,balfcx,balfay,balfby,balfcy = 0,0,0,0,0,0
def balf(t):
    retval = (balfax*(t**2) + balfbx*t + balfcx)**2 + (balfay*(t**2) + balfby*t + balfcy)**2
    return math.sqrt(retval)

def Simpson(f, a, b, n_limit, tolerance):
    n = 2
    multiplier = (b - a)/6.0
    endsum = f(a) + f(b)
    interval = (b - a)/2.0
    asum = 0.0
    bsum = f(a + interval)
    est1 = multiplier * (endsum + (2.0 * asum) + (4.0 * bsum))
    est0 = 2.0 * est1
    #print multiplier, endsum, interval, asum, bsum, est1, est0
    while n < n_limit and abs(est1 - est0) > tolerance:
        n *= 2
        multiplier /= 2.0
        interval /= 2.0
        asum += bsum
        bsum = 0.0
        est0 = est1
        for i in range(1, n, 2):
            bsum += f(a + (i * interval))
            est1 = multiplier * (endsum + (2.0 * asum) + (4.0 * bsum))
        #print multiplier, endsum, interval, asum, bsum, est1, est0
    return est1

def bezierlengthSimpson(bez, tolerance = 0.001):
    ((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)) = bez
    global balfax,balfbx,balfcx,balfay,balfby,balfcy
    ax,ay,bx,by,cx,cy,x0,y0=bezierparameterize(((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)))
    balfax,balfbx,balfcx,balfay,balfby,balfcy = 3*ax,2*bx,cx,3*ay,2*by,cy
    return Simpson(balf, 0.0, 1.0, 4096, tolerance)

def bezierparameterize(bez):
    #parametric bezier
    ((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)) = bez
    x0=bx0
    y0=by0
    cx=3*(bx1-x0)
    bx=3*(bx2-bx1)-cx
    ax=bx3-x0-cx-bx
    cy=3*(by1-y0)
    by=3*(by2-by1)-cy
    ay=by3-y0-cy-by

    return ax,ay,bx,by,cx,cy,x0,y0
    #ax,ay,bx,by,cx,cy,x0,y0=bezierparameterize(((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)))
def bezierpointatt(bez,t):
    ((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)) = bez
    ax,ay,bx,by,cx,cy,x0,y0=bezierparameterize(((bx0,by0),(bx1,by1),(bx2,by2),(bx3,by3)))
    x=ax*(t**3)+bx*(t**2)+cx*t+x0
    y=ay*(t**3)+by*(t**2)+cy*t+y0
    return x,y
    
class Point:
    def __init__(self,x,y):
        self.x=x
        self.y=y
    def __str__(self):
        return repr(self)
    def __repr__(self):
        return "Point {"+str(self.x)+","+str(self.y)+"}"
class Bezier:
    def __init__(self,points):
        self.__points=points
        self.length()
    def __pointsf(self,t):
        cx = 3.0 * (self.__points[1].x - self.__points[0].x)
        bx = 3.0 * (self.__points[3].x - self.__points[1].x) - cx
        ax = self.__points[2].x - self.__points[0].x - cx - bx
        
        cy = 3.0 * (self.__points[1].y - self.__points[0].y)
        by = 3.0 * (self.__points[3].y - self.__points[1].y) - cy
        ay = self.__points[2].y - self.__points[0].y - cy - by
        
        
        tSquared = t * t
        tCubed = tSquared * t
        
        x = (ax * tCubed) + (bx * tSquared) + (cx * t) + self.__points[0].x
        y = (ay * tCubed) + (by * tSquared) + (cy * t) + self.__points[0].y
        
        return (x,y)
    def computeBezier(self,nbpoints):
        print("NB : ",nbpoints)
        dt = 1.0 / ( nbpoints - 1 );
        points=[]
        for i in range(nbpoints-1):
            points.append(self.__pointsf(i*dt))
        print(points)
        return points
    def length(self):
        self.length = bezierlengthSimpson(((self.__points[0].x,self.__points[0].y),(self.__points[1].x,self.__points[1].y),(self.__points[2].x,self.__points[2].y),(self.__points[3].x,self.__points[3].y)))
    def __str__(self):
        return repr(self)
    def __repr__(self):
        s = "Bezier {"
        for p in self.__points:
            s+=repr(p)
        s +="}"
        return s
        
class Spline:
    def __init__(self,curves):
        self.__curves=curves
        self.__points=None
    def getpoints(self,speed):
        if(self.__points is not None):
            return self.__points
        nb = 1/speed + 1
        points = []
        tl = 0
        for c in self.__curves:
            tl += c.length
        for c in self.__curves:    
            points+=c.computeBezier(int(nb*c.length/tl))
        self.__points=points
        return points
    def __str__(self):
        return repr(self)
    def __repr__(self):
        s = "Spline {"
        for b in self.__curves:
            s+=repr(b)
        s+="}"
        return s
        