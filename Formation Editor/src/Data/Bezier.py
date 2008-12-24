#Simple point class
import math;
def sqr(x):
    return x*x;
class Point:
    def __init__(self,x,y):
        self.x = x;
        self.y = y;
    def __eq__(self,o):
        return o.x == self.x and o.y == self.y;
            
class Line:
    def __init__(self,p1,p2):
        self.p1 = p1;
        self.p2 = p2;
    def pointAt(self,t):
        vx = float(self.p2.x - self.p1.x);
        vy = float(self.p2.y - self.p1.y);
        return Point(self.p1.x + vx * float(t), self.p1.y + vy * float(t));

    
        
#Cubic Bezier Curve
class CubicBezier:
    def __init__(self,p1,p2,p3,p4):
        self.points = [];
        self.points.append(p1);
        self.points.append(p2);
        self.points.append(p3);
        self.points.append(p4);
        self.__length = 0;
    def update(self):
        self.__length = self.__BezierArcLength();
    def length(self):
        return self.__length;
    def __Simpson(self,f, a, b, n_limit, tolerance):
        n = 1
        multiplier = (b - a)/6.0
        endsum = f(a) + f(b)
        interval = (b - a)/2.0
        asum = 0.0
        bsum = f(a + interval)
        est1 = multiplier * (endsum + (2.0 * asum) + (4.0 * bsum))
        est0 = 2.0 * est1
        while n < n_limit and abs(est1 - est0) > tolerance:
            n *= 2
            multiplier /= 2.0
            interval /= 2.0
            asum += bsum
            bsum = 0.0
            est0 = est1
            for i in xrange(1, n, 2):
                bsum += f(a + (i * interval))
                est1 = multiplier * (endsum + (2.0 * asum) + (4.0 * bsum))
        return est1
    def __BezierArcLength(self):
        k1 = Point(0,0);
        k2 = Point(0,0);
        k3 = Point(0,0);
        k4 = Point(0,0);
        
        k1.x = -self.points[0].x + 3*(self.points[1].x - self.points[2].x) + self.points[3].x;
        k2.x = 3*(self.points[0].x + self.points[2].x) - 6*self.points[1].x;
        k3.x = 3*(self.points[1].x - self.points[0].x);
        k4.x = self.points[0].x;
        
        k1.y = -self.points[0].y + 3*(self.points[1].y - self.points[2].y) + self.points[3].y;
        k2.y = 3*(self.points[0].y + self.points[2].y) - 6*self.points[1].y;
        k3.y = 3*(self.points[1].y - self.points[0].y);
        k4.y = self.points[0].y;
    
        self.q1 = 9.0*(sqr(k1.x) + sqr(k1.y));
        self.q2 = 12.0*(k1.x*k2.x + k1.y*k2.y);
        self.q3 = 3.0*(k1.x*k3.x + k1.y*k3.y) + 4.0*(sqr(k2.x) + sqr(k2.y));
        self.q4 = 4.0*(k2.x*k3.x + k2.y*k3.y);
        self.q5 = sqr(k3.x) + sqr(k3.y);
    
        result = self.__Simpson(self.__balf, 0, 1, 1024, 0.001);
        return result;

    #Bezier Arc Length Function
    def __balf(self,t):
        result = self.q5 + t*(self.q4 + t*(self.q3 + t*(self.q2 + t*self.q1)));
        result = math.sqrt(result);
        return result;

    #returns (x,y) tuple (-1,-1) if error
    def getPoint(self,t):
        x=-1
        y=-1
        if(t >= 0 and t <= 1):
            #Square
            t2 = t*t;
            #Cube
            t3 = t2*t;
            
            #Coeff 
            a = (-t3+3*t2-3*t+1);
            b = (3*t3-6*t2+3*t);
            c = (3*t2-3*t3);
            d = t3;
            
            #calcul
            x = a * self.points[0].x + b * self.points[1].x + c * self.points[2].x + d * self.points[3].x; 
            y = a * self.points[0].y + b * self.points[1].y + c * self.points[2].y + d * self.points[3].y; 
        
        return (x,y)
    
class BezierSpline:
    def __init__(self):
        self.__curves=[];
        self.__length = 0;
    
    def getCurveCount(self):
        return len(self.__curves);
    def getCurve(self,index):
        if(index >= 0 and index < self.getCurveCount()):
            return self.__curves[index];
        return None;
    #if last point of the spline does not match with the first of c add nothing
    def addCurve(self,c):
        #Not first Curve
        if(len(self.__curves)>0):
            if(self.__curves[-1].points[-1] != c.points[0]):
                return;
        self.__curves.append(c);
    def update(self):
        total = 0;
        for c in self.__curves:
            c.update();
            total += c.length();
        self.__length = total;
    def length(self):
        return self.__length;
        
    #t from 0 to 1
    def getPoint(self,t):
        totalper = 0;
        n = 0;
        per = 0;
        for c in self.__curves:
            per = c.length()/self.length();
            totalper += per;
            if(t < totalper):
                break;
            n += 1;
        totalper -= per;
        rt = (t - totalper)/per;
        return self.__curves[n].getPoint(rt);