from PyQt4.QtGui import *
from PyQt4.QtCore import *
from Data.Data import *
CurveMode = 0
LineMode = 1
class PathStrokeRenderer(QWidget):
    ENEMY_BASE_SIZE = 30;
    ENEMY_COLOR = [Qt.red,Qt.yellow,Qt.green,Qt.blue,QColor(255.0,0.0,255.0)];
    def __init__(self,parent):
        QWidget.__init__(self,parent)
        self.m_curve=True;
        self.m_colors=[Qt.red,Qt.blue,Qt.black,Qt.magenta,Qt.green,Qt.yellow,Qt.cyan];
        self.currentPath = None;
        self.currentPoint = None;
        self.m_pointSize = 5;
        self.m_activePoint = -1;
        self.m_capStyle = Qt.FlatCap;
        self.m_joinStyle = Qt.BevelJoin;
        self.m_pathMode = CurveMode;
        self.m_penWidth = 1;
        self.m_penStyle = Qt.SolidLine;
        self.setSizePolicy(QSizePolicy.Expanding, QSizePolicy.Expanding);
        self.setFixedSize(800,600)
        self.Animation = False;
        self.AnimEngine = None;
    def paintEvent(self,e):
        static_image = 0;
        painter = QPainter();
        static_image = QImage(self.size(), QImage.Format_RGB32);
        painter.begin(static_image);

        painter.drawPixmap(240,50,QPixmap(":res/images/demi.png"))
    
    
        painter.setRenderHint(QPainter.Antialiasing);
    
        # client painting
    
        self.paint(painter);
    
        painter.end();
        painter.begin(self);
        painter.drawImage(e.rect(), static_image, e.rect());
    
    def paint(self,painter):

        if(not self.Animation):
            nbPaths = SData.getInstance().getNbEnemyPath();
            
            for i in xrange(nbPaths):
                ePath = SData.getInstance().getEnemyPath(i).BezierSpline;
                path = QPainterPath();
                count = ePath.getCurveCount();
                if(count == 0):
                    continue;
                path.moveTo(ePath.getCurve(0).points[0].x,ePath.getCurve(0).points[0].y);
                lg = self.m_colors[i%len(self.m_colors)];
                for j in xrange(count):
                    curve = ePath.getCurve(j);
                    path.cubicTo(curve.points[1].x,curve.points[1].y,curve.points[2].x,curve.points[2].y,curve.points[3].x,curve.points[3].y);
                    for k in xrange(0,4):
                        #Control Points
                        painter.setPen(QColor(50, 100, 120, 200));
                        painter.setBrush(QColor(50, 100, 120, 120));
                        painter.drawEllipse(QRectF(curve.points[k].x - self.m_pointSize,curve.points[k].y - self.m_pointSize,self.m_pointSize*2, self.m_pointSize*2));
                        
                        if(k > 0):
                            painter.setPen(lg);
                            painter.setBrush(Qt.NoBrush);
                                            
                            #Control Line
                            painter.setPen(QPen(Qt.black, 0, Qt.SolidLine));
                            painter.drawLine(curve.points[k-1].x,curve.points[k-1].y,curve.points[k].x,curve.points[k].y);
                
                pen = QPen(lg, self.m_penWidth, self.m_penStyle, self.m_capStyle, self.m_joinStyle);
                painter.strokePath(path, pen);
        else:
            self.Animation = self.AnimEngine.GlobalAnimation();
            nbM = SData.getInstance().getNbMonster();
            for i in xrange(nbM):
                pos = (SData.getInstance().getMonster(i).x,SData.getInstance().getMonster(i).y);
                painter.setPen(PathStrokeRenderer.ENEMY_COLOR[SData.getInstance().getMonster(i).life]);
                painter.setBrush(PathStrokeRenderer.ENEMY_COLOR[SData.getInstance().getMonster(i).life]);
                offset = PathStrokeRenderer.ENEMY_BASE_SIZE * SData.getInstance().getMonster(i).size / 2.0;
                painter.drawEllipse(QRect(pos[0]-offset,pos[1]-offset,PathStrokeRenderer.ENEMY_BASE_SIZE * SData.getInstance().getMonster(i).size,PathStrokeRenderer.ENEMY_BASE_SIZE * SData.getInstance().getMonster(i).size));
    def getCurrentPath(self):
        return self.currentPath;
    def mousePressEvent(self,e):
        
        nbPaths = SData.getInstance().getNbEnemyPath();
        distance = -1;
        currentPoint = None;
        currentPath = None;
        for i in xrange(nbPaths):
            path = SData.getInstance().getEnemyPath(i);
            ePath = path.BezierSpline;
            count = ePath.getCurveCount();
            for j in xrange(count):
                curve = ePath.getCurve(j);
                for k in xrange(4):
                    d = QLineF(e.pos().x(),e.pos().y(),curve.points[k].x,curve.points[k].y).length();
                    if ((distance < 0 and d < 8 * self.m_pointSize) or d < distance):
                        currentPoint = curve.points[k];
                        currentPath = path;
                        distance = d;
        
        if(distance != -1):
            self.currentPath=currentPath;
            self.currentPoint=currentPoint;
            self.mouseMoveEvent(e);
    
    def mouseDoubleClickEvent(self,e):
        if(self.currentPath is not None):
            self.currentPath.AddPoint(Point(e.pos().x(),e.pos().y()));
            self.update();

    def mouseMoveEvent(self,e):
        if (self.currentPoint is not None):
            self.currentPoint.x = e.pos().x();
            self.currentPoint.y = e.pos().y();
            self.update();

    def mouseReleaseEvent(self,e):
        self.currentPoint = None;

    def setPenWidth(self,penWidth):
        self.m_penWidth = penWidth / 10.0; 
        self.update();

    def setFlatCap(self):
        self.m_capStyle = Qt.FlatCap; 
        self.update();
    
    def setSquareCap(self):
        self.m_capStyle = Qt.SquareCap; 
        self.update();
    
    def setRoundCap(self):
        self.m_capStyle = Qt.RoundCap; 
        self.update();

    def setBevelJoin(self):
        self.m_joinStyle = Qt.BevelJoin;
        self.update();
        
    def setMiterJoin(self):
        self.m_joinStyle = Qt.MiterJoin;
        self.update();
    
    def setRoundJoin(self):
        self.m_joinStyle = Qt.RoundJoin;
        self.update();

    def setCurveMode(self):
        self.m_pathMode = CurveMode;
        self.update();
        
    def setLineMode(self):
        self.m_pathMode = LineMode;
        self.update();

    def setSolidLine(self):
        self.m_penStyle = Qt.SolidLine;
        self.update();
        
    def setDashLine(self):
        self.m_penStyle = Qt.DashLine;
        self.update();
        
    def setDotLine(self):
        self.m_penStyle = Qt.DotLine; 
        self.update();
        
    def setDashDotLine(self):
        self.m_penStyle = Qt.DashDotLine;
        self.update();
        
    def setDashDotDotLine(self):
        self.m_penStyle = Qt.DashDotDotLine;
        self.update();
        
    def setCustomDashLine(self):
        self.m_penStyle = Qt.NoPen; 
        self.update();
        
class PathStrokeWidget(QWidget):
    def __init__(self):
        QWidget.__init__(self)
        self.setWindowTitle("Path Stroking");
        
        #Setting up palette.
        pal = self.palette()
        self.setPalette(pal);
    
        # Widget construction and property setting
        self.m_renderer = PathStrokeRenderer(self);
    
        mainGroup = QGroupBox(self);
        mainGroup.setFixedWidth(180);
        mainGroup.setTitle("Path Stroking");
    
        capGroup = QGroupBox(mainGroup);
        flatCap = QRadioButton(capGroup);
        squareCap = QRadioButton(capGroup);
        roundCap = QRadioButton(capGroup);
        capGroup.setTitle("Cap Style");
        flatCap.setText("Flat Cap");
        squareCap.setText("Square Cap");
        roundCap.setText("Round Cap");
    
        joinGroup = QGroupBox(mainGroup);
        bevelJoin = QRadioButton(joinGroup);
        miterJoin = QRadioButton(joinGroup);
        roundJoin = QRadioButton(joinGroup);
        joinGroup.setTitle("Join Style");
        bevelJoin.setText("Bevel Join");
        miterJoin.setText("Miter Join");
        roundJoin.setText("Round Join");
    
        styleGroup = QGroupBox(mainGroup);
        solidLine = QRadioButton(styleGroup);
        dashLine = QRadioButton(styleGroup);
        dotLine = QRadioButton(styleGroup);
        dashDotLine = QRadioButton(styleGroup);
        dashDotDotLine = QRadioButton(styleGroup);
        customDashLine = QRadioButton(styleGroup);
        styleGroup.setTitle("Pen Style");

        line_solid = QPixmap(":res/images/line_solid.png");
        solidLine.setIcon(QIcon(line_solid));
        solidLine.setIconSize(line_solid.size());
        line_dashed = QPixmap(":res/images/line_dashed.png");
        dashLine.setIcon(QIcon(line_dashed));
        dashLine.setIconSize(line_dashed.size());
        line_dotted = QPixmap(":res/images/line_dotted.png");
        dotLine.setIcon(QIcon(line_dotted));
        dotLine.setIconSize(line_dotted.size());
        line_dash_dot = QPixmap(":res/images/line_dash_dot.png");
        dashDotLine.setIcon(QIcon(line_dash_dot));
        dashDotLine.setIconSize(line_dash_dot.size());
        line_dash_dot_dot=QPixmap(":res/images/line_dash_dot_dot.png");
        dashDotDotLine.setIcon(QIcon(line_dash_dot_dot));
        dashDotDotLine.setIconSize(line_dash_dot_dot.size());
        customDashLine.setText("Custom Style");
    
        fixedHeight = bevelJoin.sizeHint().height();
        solidLine.setFixedHeight(fixedHeight);
        dashLine.setFixedHeight(fixedHeight);
        dotLine.setFixedHeight(fixedHeight);
        dashDotLine.setFixedHeight(fixedHeight);
        dashDotDotLine.setFixedHeight(fixedHeight);

    
        pathModeGroup = QGroupBox(mainGroup);
        curveMode = QRadioButton(pathModeGroup);
        lineMode = QRadioButton(pathModeGroup);
        pathModeGroup.setTitle("Path composed of");
        curveMode.setText("Curves");
        lineMode.setText("Lines");
    
        penWidthGroup = QGroupBox(mainGroup);
        penWidth = QSlider(Qt.Horizontal, penWidthGroup);
        penWidth.setSizePolicy(QSizePolicy.Preferred, QSizePolicy.Fixed);
        penWidthGroup.setTitle("Pen Width");
        penWidth.setRange(0, 500);
    
        animated = QPushButton(mainGroup);
        animated.setText("Animate");
        animated.setCheckable(True);
    
        showSourceButton = QPushButton(mainGroup);
        showSourceButton.setText("Show Source");

        whatsThisButton = QPushButton(mainGroup);
        whatsThisButton.setText("What's This?");
        whatsThisButton.setCheckable(True);
    
        # Layouting
        viewLayout = QHBoxLayout(self);
        viewLayout.addWidget(self.m_renderer);
        viewLayout.addWidget(mainGroup);
    
        mainGroupLayout = QVBoxLayout(mainGroup);
        mainGroupLayout.setMargin(3);
        mainGroupLayout.addWidget(capGroup);
        mainGroupLayout.addWidget(joinGroup);
        mainGroupLayout.addWidget(styleGroup);
        mainGroupLayout.addWidget(penWidthGroup);
        mainGroupLayout.addWidget(pathModeGroup);
        mainGroupLayout.addWidget(animated);
        mainGroupLayout.addStretch(1);
        mainGroupLayout.addWidget(showSourceButton);
        mainGroupLayout.addWidget(whatsThisButton);
    
        capGroupLayout = QVBoxLayout(capGroup);
        capGroupLayout.addWidget(flatCap);
        capGroupLayout.addWidget(squareCap);
        capGroupLayout.addWidget(roundCap);
    
        joinGroupLayout = QVBoxLayout(joinGroup);
        joinGroupLayout.addWidget(bevelJoin);
        joinGroupLayout.addWidget(miterJoin);
        joinGroupLayout.addWidget(roundJoin);
    
        styleGroupLayout = QVBoxLayout(styleGroup);
        styleGroupLayout.addWidget(solidLine);
        styleGroupLayout.addWidget(dashLine);
        styleGroupLayout.addWidget(dotLine);
        styleGroupLayout.addWidget(dashDotLine);
        styleGroupLayout.addWidget(dashDotDotLine);
        styleGroupLayout.addWidget(customDashLine);
    
        pathModeGroupLayout = QVBoxLayout(pathModeGroup);
        pathModeGroupLayout.addWidget(curveMode);
        pathModeGroupLayout.addWidget(lineMode);
    
        penWidthLayout = QVBoxLayout(penWidthGroup);
        penWidthLayout.addWidget(penWidth);
    
        # Set up connections
        QObject.connect(penWidth, SIGNAL("valueChanged(int)"),self.m_renderer.setPenWidth);
    
        QObject.connect(flatCap, SIGNAL("clicked()"),self.m_renderer.setFlatCap);
        QObject.connect(squareCap, SIGNAL("clicked()"),self.m_renderer.setSquareCap);
        QObject.connect(roundCap, SIGNAL("clicked()"),self.m_renderer.setRoundCap);
    
        QObject.connect(bevelJoin, SIGNAL("clicked()"),self.m_renderer.setBevelJoin);
        QObject.connect(miterJoin, SIGNAL("clicked()"),self.m_renderer.setMiterJoin);
        QObject.connect(roundJoin, SIGNAL("clicked()"),self.m_renderer.setRoundJoin);
    
        QObject.connect(curveMode, SIGNAL("clicked()"),self.m_renderer.setCurveMode);
        QObject.connect(lineMode, SIGNAL("clicked()"),self.m_renderer.setLineMode);
    
        QObject.connect(solidLine, SIGNAL("clicked()"),self.m_renderer.setSolidLine);
        QObject.connect(dashLine, SIGNAL("clicked()"),self.m_renderer.setDashLine);
        QObject.connect(dotLine, SIGNAL("clicked()"),self.m_renderer.setDotLine);
        QObject.connect(dashDotLine, SIGNAL("clicked()"),self.m_renderer.setDashDotLine);
        QObject.connect(dashDotDotLine, SIGNAL("clicked()"),self.m_renderer.setDashDotDotLine);
        QObject.connect(customDashLine, SIGNAL("clicked()"),self.m_renderer.setCustomDashLine);
        
    
        # Set the defaults
        animated.setChecked(True);
        flatCap.setChecked(True);
        bevelJoin.setChecked(True);
        penWidth.setValue(50);
        curveMode.setChecked(True);
        solidLine.setChecked(True);


