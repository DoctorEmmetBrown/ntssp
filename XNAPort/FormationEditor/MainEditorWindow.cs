using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.GamerServices;
using DisplayEngine;
using DisplayEngine.Display2D;
using AnimationEngine;
using Data;
using Utils;
using Utils.Math;

namespace FormationEditor
{
    public partial class MainEditorWindow : Form
    {
        Formation mFormation = new Formation();
        CubicBezier mCurrentSpline = null;
        CubicBezier mNextCurrentSpline = null;
        CubicBezier mPrevCurrentSpline = null;
        BezierSpline mCurrentParentSpline = null;
        int mPointNumber = -1;

        bool mAnimate = false;
        Engine mCurrentEngine = null;

        public MainEditorWindow()
        {
            InitializeComponent();
            GameHandle.MouseDown += new MouseEventHandler(MouseDown);
            GameHandle.MouseMove += new MouseEventHandler(MouseMove);
            GameHandle.MouseUp += new MouseEventHandler(MouseUp);
            GameHandle.MouseDoubleClick += new MouseEventHandler(MouseDoubleClick);
        }
        
        public void Render(GameTime gameTime)
        {


            if (mAnimate)
            {
                if (mCurrentEngine == null)
                {
                    mCurrentEngine = new Engine(mFormation);
                }

                mCurrentEngine.GlobalAnimation(gameTime);


            }

            foreach (BezierSpline spline in mFormation.Splines)
            {
                Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(0, 0, 800, 600);
                spline.Draw(rect);
                spline.DrawControlPoints(rect);
            }

        }

        void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mCurrentSpline != null)
            {
                int x = (int)((((float)e.X) / ((float)GameHandle.Width)) * 100.0f);
                int y = (int)((((float)e.Y) / ((float)GameHandle.Height)) * 100.0f);

                Vector2 vect = new Vector2(x,y);
                CubicBezier bez = mCurrentParentSpline.GetCurve(mCurrentParentSpline.CurveCount - 1);
                Vector2 prev = bez.Points[3];

                Vector2 p1 = Vector2.Lerp(prev, vect, 0.3f);
                Vector2 p2 = Vector2.Lerp(prev, vect, 0.6f);

                mCurrentParentSpline.AddCurve(new CubicBezier(prev, p1, p2, vect));
            }
        }

        void MouseUp(object sender, MouseEventArgs e)
        {
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (mCurrentSpline != null && e.Button == MouseButtons.Left)
            {
                int x = (int)((((float)e.X) / ((float)GameHandle.Width)) * 100.0f);
                int y = (int)((((float)e.Y) / ((float)GameHandle.Height)) * 100.0f);

                mCurrentSpline.Points[mPointNumber - 1].X = x;
                mCurrentSpline.Points[mPointNumber - 1].Y = y;
                if (mPointNumber == 1)
                {
                    if (mPrevCurrentSpline != null)
                    {
                        mPrevCurrentSpline.Points[3].X = x;
                        mPrevCurrentSpline.Points[3].Y = y;
                    }
                }

                if (mPointNumber == 4)
                {
                    if (mNextCurrentSpline != null)
                    {
                        mNextCurrentSpline.Points[0].X = x;
                        mNextCurrentSpline.Points[0].Y = y;
                    }
                }

                this.mCurrentParentSpline.Update();
            }
        }

        void MouseDown(object sender, MouseEventArgs e)
        {
      //      Vector2 coord = new Vector2((e.X / GameHandle.Width) * 100, (e.Y / GameHandle.Height) * 100);

            int x = (int)((((float)e.X) / ((float)GameHandle.Width)) * 100.0f);
            int y = (int)((((float)e.Y) / ((float)GameHandle.Height)) * 100.0f);
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(x,y, 3, 3);

            mPrevCurrentSpline = null;
            mNextCurrentSpline = null;
            foreach (BezierSpline spline in mFormation.Splines)
            {
                for (int i = 0; i < spline.CurveCount; i++)
                {
                    int pointNum = 0;
                    CubicBezier bez = spline.GetCurve(i);
                    for (int j = 0; j < bez.Points.Length; j++)
                    {
                        pointNum++;
                        Microsoft.Xna.Framework.Rectangle test = new Microsoft.Xna.Framework.Rectangle((int)(bez.Points[j].X - 1),(int) (bez.Points[j].Y - 1), 3, 3);

                        if (test.Intersects(rect))
                        {
                            mCurrentSpline = bez;
                            if ((i + 1) < spline.CurveCount)
                            {
                                mNextCurrentSpline = spline.GetCurve(i + 1);
                            }
                            if (i > 0)
                            {
                                mPrevCurrentSpline = spline.GetCurve(i - 1);
                            }
                            mCurrentParentSpline = spline;
                            mPointNumber = pointNum;
                        }
                    }
                }
            }
        }

        private void RefreshPaths()
        {
            PathsListView.Nodes.Clear();
            foreach (BezierSpline spline in mFormation.Splines)
            {
                string values = "";

                values += "Path " + spline.GUID + " ";
                values += "Bezier Spline";
                TreeNode item = new TreeNode(values);

                if (mFormation.SplinesAssociations.Keys.Contains(spline) && mFormation.SplinesAssociations[spline].Count > 0)
                {
                    TreeNode Associations = new TreeNode("Associations");

                    foreach (Association m in mFormation.SplinesAssociations[spline])
                    {
                        TreeNode monster = new TreeNode(m.ToString());
                        Associations.Nodes.Add(monster);
                    }
                    item.Nodes.Add(Associations);
                }

                PathsListView.Nodes.Add(item);
            }
        }
        public IntPtr GetGameWindowHandle()
        {
            return GameHandle.Handle;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RemovePathButton_Click(object sender, EventArgs e)
        {
            mFormation.Splines.Remove(mCurrentParentSpline);

            RefreshPaths();

            mCurrentParentSpline = null;
            mCurrentSpline = null;
            mNextCurrentSpline = null;
            mPrevCurrentSpline = null;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GameHandle_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SpeedLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void AddPathButton_Click(object sender, EventArgs e)
        {
            BezierSpline spline = new BezierSpline();
            spline.AddCurve(new CubicBezier(new Vector2(50.0f,0.0f),new Vector2(50.0f,10.0f),new Vector2(50.0f,20.0f),new Vector2(50.0f,30.0f)));
            mFormation.Splines.Add(spline);
            RefreshPaths();
        }

        private void ColorSlider_Scroll(object sender, EventArgs e)
        {
            ColorLabel.Text = "Color :" + ColorSlider.Value;
        }

        private void SizeSlider_Scroll(object sender, EventArgs e)
        {
            SizeLabel.Text = "Size :" + SizeSlider.Value;
        }

        private void RefreshMonsters()
        {
            MonstersListView.Nodes.Clear();
            MonstersListView.SelectedNodes.Clear();
            foreach (Monster m in mFormation.Monsters)
            {
                TreeNode item = new TreeNode(m.ToString());

                if(m.Groups.Count > 0)
                {
                    TreeNode groups = new TreeNode("Groups");

                    foreach (Group g in m.Groups)
                    {
                        TreeNode group = new TreeNode(g.ToString());
                        groups.Nodes.Add(group);
                    }

                    item.Nodes.Add(groups);
                }
                MonstersListView.Nodes.Add(item);
            }
        }

        private void AddMonsterButton_Click(object sender, EventArgs e)
        {
            Monster m = new Monster(SizeSlider.Value, ColorSlider.Value);
            mFormation.Monsters.Add(m);
            RefreshAll();
        }

        private void DeleteMonsterButton_Click(object sender, EventArgs e)
        {
            List<Monster> toDelete = GetSelectedMonsters();

            foreach (Monster m in toDelete)
            {
                mFormation.Monsters.Remove(m);
            }

            foreach (Group g in mFormation.Groups)
            {
                foreach (Monster m in toDelete)
                {
                    g.Monsters.Remove(m);
                    g.MonstersOrder.Remove(m);
                }
            }
            RefreshAll();
        }

        private void ChangeMonsterButton_Click(object sender, EventArgs e)
        {
            List<Monster> toDelete = GetSelectedMonsters();
            foreach (Monster m in toDelete)
            {
                m.Color = ColorSlider.Value;
                m.Size = SizeSlider.Value;
            }
            RefreshAll();
        }

        private void SpeedSlider_Scroll(object sender, EventArgs e)
        {
            SpeedLabel.Text = "Speed: " + SpeedSlider.Value + "%";
        }

        private void DiffTimeSlider_Scroll(object sender, EventArgs e)
        {
            DiffTimeLabel.Text = "DiffTime: " + ((float)(DiffTimeSlider.Value)/1000.0f) + "s";
        }

        private void RefreshGroups()
        {
            GlobalGroupOrderListView.Nodes.Clear();
            GroupsListView.Nodes.Clear();
            AssociationGroupsListView.Nodes.Clear();
            foreach (Group g in mFormation.Groups)
            {
                TreeNode item = new TreeNode(g.ToString());

                if (g.Monsters.Count > 0)
                {
                    TreeNode monsters = new TreeNode("Monsters");

                    foreach (Monster m in g.Monsters.Keys)
                    {
                        TreeNode monster = new TreeNode(m.ToString());
                        monsters.Nodes.Add(monster);
                    }
                    item.Nodes.Add(monsters);
                }

                if (g.Associations.Count > 0)
                {
                    TreeNode Associations = new TreeNode("Associations");

                    foreach (Association a in g.Associations)
                    {
                        TreeNode assoc = new TreeNode(a.ToString());
                        Associations.Nodes.Add(assoc);
                    }

                    item.Nodes.Add(Associations);
                }
                GroupsListView.Nodes.Add(item);
                TreeNode item2 = (TreeNode)item.Clone();
                AssociationGroupsListView.Nodes.Add(item2);
                TreeNode item3 = (TreeNode)item.Clone();
                GlobalGroupOrderListView.Nodes.Add(item3);
            }
        }

        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            List<Monster> toDelete = GetSelectedMonsters();

            if (toDelete.Count == 0)
            {
                return;
            }

            EffectType type = EffectType.Zero;

            switch((string)EffectTypeComboBox.SelectedItem)
            {
                case "Normal":
                    type = EffectType.Zero;
                    break;
                case "Circle":
                    type = EffectType.Circle;
                    break;
                case "Switch":
                    type = EffectType.Switch;
                    break;
                case "Arc":
                    type = EffectType.Arc;
                    break;
                case "Fixed":
                    type = EffectType.Fixed;
                    break;
            }
            Group g = new Group(type, ((float)SpeedSlider.Value) / 100.0f, ((float)(DiffTimeSlider.Value) / 1000.0f));
            

            foreach (Monster m in toDelete)
            {
                g.Monsters.Add(m,new Vector2());
                g.MonstersOrder.Add(m);
                m.Groups.Add(g);
            }

            mFormation.Groups.Add(g);
            RefreshAll();
        }

        private List<Monster> GetSelectedMonsters()
        {
            List<Monster> list = new List<Monster>();
            foreach (TreeNode item in MonstersListView.SelectedNodes)
            {
                char [] sep = new char[1];
                sep[0] = ' ';
                if (!item.Text.StartsWith("Monster"))
                {
                    return new List<Monster>();
                }
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Monster> toDelete = new List<Monster>();
                foreach (Monster m in mFormation.Monsters)
                {
                    if (m.GUID == id)
                    {
                        toDelete.Add(m);
                    }
                }
                foreach (Monster m in toDelete)
                {
                    list.Add(m);
                }
            }

            return list;
        }

        private void EffectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void DeleteGroupButton_Click(object sender, EventArgs e)
        {
            TreeNode item = GroupsListView.SelectedNode;
            if (!item.Text.StartsWith("Group"))
            {
                return;
            }

            char[] sep = new char[1];
            sep[0] = ' ';
            int id = Convert.ToInt32(item.Text.Split(sep)[1]);

            List<Group> toDelete = new List<Group>();
            foreach (Group g in mFormation.Groups)
            {
                if (g.GUID == id)
                {
                    toDelete.Add(g);
                }
            }
            foreach (Group m in toDelete)
            {
                mFormation.Groups.Remove(m);
            }

            foreach (Monster m in mFormation.Monsters)
            {
                foreach (Group g in toDelete)
                {
                    m.Groups.Remove(g);
                }
            }
            RefreshAll();
        }

        private void RefreshAll()
        {
            RefreshGroups();
            RefreshMonsters();
            RefreshPaths();
            RefreshAssociations();
            CurrentOrderListView.Nodes.Clear();
        }

        private void ChangeGroupButton_Click(object sender, EventArgs e)
        {
            List<Monster> selected = GetSelectedMonsters();

            if (selected.Count == 0)
            {
                return;
            }

            TreeNode item = GroupsListView.SelectedNode;

            if (!item.Text.StartsWith("Group"))
            {
                return;
            }
            char[] sep = new char[1];
            sep[0] = ' ';
            int id = Convert.ToInt32(item.Text.Split(sep)[1]);

            List<Group> toDelete = new List<Group>();
            foreach (Group g in mFormation.Groups)
            {
                if (g.GUID == id)
                {
                    toDelete.Add(g);
                }
            }
            foreach (Group m in toDelete)
            {
                m.Speed = ((float)SpeedSlider.Value) / 100.0f;
                m.DiffTime = ((float)(DiffTimeSlider.Value) / 1000.0f);

                foreach (Monster mon in m.Monsters.Keys)
                {
                    mon.Groups.Remove(m);
                }
                m.Monsters.Clear();
                m.MonstersOrder.Clear();

                foreach (Monster mon in selected)
                {
                    m.Monsters.Add(mon,new Vector2());
                    m.MonstersOrder.Add(mon);
                    mon.Groups.Remove(m);
                }
            }
            RefreshAll();
        }

        private void AssociateButton_Click(object sender, EventArgs e)
        {
            TreeNode item = AssociationGroupsListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Group"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Group> toDelete = new List<Group>();
                foreach (Group g in mFormation.Groups)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                TreeNode item2 = PathsListView.SelectedNode;

                if (item2 != null)
                {
                    if (!item2.Text.StartsWith("Path"))
                    {
                        return;
                    }

                    int id2 = Convert.ToInt32(item2.Text.Split(sep)[1]);

                    List<BezierSpline> toAssoc = new List<BezierSpline>();
                    foreach (BezierSpline spline in mFormation.Splines)
                    {
                        if (spline.GUID == id2)
                        {
                            toAssoc.Add(spline);
                        }
                    }

                    if (toDelete.Count > 0 && toAssoc.Count > 0)
                    {
                        AssociationType type = AssociationType.Normal;
                        switch(AssociationTypeComboBox.SelectedText)
                        {
                            case "Normal":
                                type = AssociationType.Normal;
                                break;
                            case "When Player is Found":
                                type = AssociationType.WhenPlayerIsFound;
                                break;
                        }

                        Association assoc = new Association(type, toDelete[0], toAssoc[0], ((float)(TimeBeforeSlider.Value) / 1000.0f));

                        mFormation.Associations.Add(assoc);
                        if (!mFormation.SplinesAssociations.Keys.Contains(toAssoc[0]))
                        {
                            mFormation.SplinesAssociations.Add(toAssoc[0],new SharedResourceList<Association>());
                        }
                        mFormation.SplinesAssociations[toAssoc[0]].Add(assoc);

                        RefreshAll();
                    }
                }


            }
        }

        private void RefreshAssociations()
        {
            AssociationListView.Nodes.Clear();
            foreach (Association a in mFormation.Associations)
            {
                TreeNode item = new TreeNode(a.ToString());
                AssociationListView.Nodes.Add(item);
            }
        }
        private void TimeBeforeSlider_Scroll(object sender, EventArgs e)
        {
            TimeBeforeLabel.Text = "Time Before: " + ((float)(TimeBeforeSlider.Value) / 1000.0f) + "s";
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            TreeNode item = AssociationListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Association"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Association> toDelete = new List<Association>();
                foreach (Association g in mFormation.Associations)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                foreach (Association a in toDelete)
                {
                    a.Group.Associations.Remove(a);
                    mFormation.SplinesAssociations[a.Path].Remove(a);
                    mFormation.Associations.Remove(a);
                }

                RefreshAll();
            }

        }

        private void GlobalGroupOrderListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode item = GlobalGroupOrderListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Group"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Group> toDelete = new List<Group>();
                foreach (Group g in mFormation.Groups)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                if(toDelete.Count > 0)
                {
                    CurrentOrderListView.Nodes.Clear();
                    foreach (Association a in toDelete[0].Associations)
                    {
                        TreeNode node = new TreeNode(a.ToString());
                        CurrentOrderListView.Nodes.Add(node);
                    }
                }
            }

        }

        private void GlobalUpButton_Click(object sender, EventArgs e)
        {
            TreeNode item = GlobalGroupOrderListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Group"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Group> toDelete = new List<Group>();
                foreach (Group g in mFormation.Groups)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                if (toDelete.Count > 0)
                {
                    int index = mFormation.Groups.IndexOf(toDelete[0]);
                    if (index > 0)
                    {
                        Group tmp = mFormation.Groups[index - 1];
                        mFormation.Groups[index - 1] = toDelete[0];
                        mFormation.Groups[index] = tmp;
                    }
                }
            }
            RefreshGroups();
        }

        private void GlobalDownButton_Click(object sender, EventArgs e)
        {
            TreeNode item = GlobalGroupOrderListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Group"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Group> toDelete = new List<Group>();
                foreach (Group g in mFormation.Groups)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                if (toDelete.Count > 0)
                {
                    int index = mFormation.Groups.IndexOf(toDelete[0]);
                    if (index < (mFormation.Groups.Count - 1))
                    {
                        Group tmp = mFormation.Groups[index + 1];
                        mFormation.Groups[index + 1] = toDelete[0];
                        mFormation.Groups[index] = tmp;
                    }
                }
            }
            RefreshGroups();
        }

        private void UpCurrentOrderButton_Click(object sender, EventArgs e)
        {
            TreeNode item = CurrentOrderListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Association"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Association> toDelete = new List<Association>();
                foreach (Association g in mFormation.Associations)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                TreeNode item2 = GlobalGroupOrderListView.SelectedNode;

                if (item2 != null)
                {
                    if (!item2.Text.StartsWith("Group"))
                    {
                        return;
                    }

                    id = Convert.ToInt32(item.Text.Split(sep)[1]);

                    List<Group> toDelete2 = new List<Group>();
                    foreach (Group g in mFormation.Groups)
                    {
                        if (g.GUID == id)
                        {
                            toDelete2.Add(g);
                        }
                    }

                    if (toDelete2.Count > 0)
                    {
                        int index = toDelete2[0].Associations.IndexOf(toDelete[0]);
                        if (index > 0)
                        {
                            Association tmp = toDelete2[0].Associations[index - 1];
                            toDelete2[0].Associations[index - 1] = toDelete[0];
                            toDelete2[0].Associations[index] = tmp;
                        }
                    }
                }
            }
            GlobalGroupOrderListView_AfterSelect(new object(),new TreeViewEventArgs(new TreeNode()));
        }

        private void DownCurrentOrderButton_Click(object sender, EventArgs e)
        {
            TreeNode item = CurrentOrderListView.SelectedNode;

            if (item != null)
            {
                if (!item.Text.StartsWith("Association"))
                {
                    return;
                }

                char[] sep = new char[1];
                sep[0] = ' ';
                int id = Convert.ToInt32(item.Text.Split(sep)[1]);

                List<Association> toDelete = new List<Association>();
                foreach (Association g in mFormation.Associations)
                {
                    if (g.GUID == id)
                    {
                        toDelete.Add(g);
                    }
                }

                TreeNode item2 = GlobalGroupOrderListView.SelectedNode;

                if (item2 != null)
                {
                    if (!item2.Text.StartsWith("Group"))
                    {
                        return;
                    }

                    id = Convert.ToInt32(item.Text.Split(sep)[1]);

                    List<Group> toDelete2 = new List<Group>();
                    foreach (Group g in mFormation.Groups)
                    {
                        if (g.GUID == id)
                        {
                            toDelete2.Add(g);
                        }
                    }

                    if (toDelete2.Count > 0)
                    {
                        int index = toDelete2[0].Associations.IndexOf(toDelete[0]);
                        if (index < (toDelete2[0].Associations.Count - 1))
                        {
                            Association tmp = toDelete2[0].Associations[index + 1];
                            toDelete2[0].Associations[index + 1] = toDelete[0];
                            toDelete2[0].Associations[index] = tmp;
                        }
                    }
                }
            }
            GlobalGroupOrderListView_AfterSelect(new object(), new TreeViewEventArgs(new TreeNode()));
        }

        private void EditGroupButton_Click(object sender, EventArgs e)
        {
            TreeNode item = GroupsListView.SelectedNode;

            if (item == null || !item.Text.StartsWith("Group"))
            {
                return;
            }
            char[] sep = new char[1];
            sep[0] = ' ';
            int id = Convert.ToInt32(item.Text.Split(sep)[1]);

            List<Group> toDelete = new List<Group>();
            foreach (Group g in mFormation.Groups)
            {
                if (g.GUID == id)
                {
                    toDelete.Add(g);
                }
            }

            if (toDelete.Count > 0)
            {
                GroupEditor editor = new GroupEditor(toDelete[0]);
                editor.ShowDialog();
            }
        }

        private void AnimateButton_Click(object sender, EventArgs e)
        {
            mAnimate = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\";
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = saveFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            XmlWriterSettings s = new XmlWriterSettings();
                            s.Indent = true;
                            XmlWriter writer = XmlWriter.Create(myStream,s);
                            IntermediateSerializer.Serialize<Formation>(writer,mFormation,null);
                            writer.Close();
                            myStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            mFormation = null;
                            mCurrentSpline = null;
                            mNextCurrentSpline = null;
                            mPrevCurrentSpline = null;
                            mCurrentParentSpline = null;
                            mPointNumber = -1;

                            mAnimate = false;
                            mCurrentEngine = null;
                            XmlReader reader = XmlReader.Create(myStream);
                            mFormation = IntermediateSerializer.Deserialize<Formation>(reader,null);

                            RefreshAll();

                            foreach (BezierSpline spline in mFormation.Splines)
                            {
                                spline.Update();
                            }

                            reader.Close();
                            myStream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

    }
}
