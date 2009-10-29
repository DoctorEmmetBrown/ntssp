using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils.Math;
using AnimationEngine;
using Data;
using DisplayEngine;
using DisplayEngine.Display2D;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace FormationEditor
{
    public partial class GroupEditor : Form
    {
        Monster mCurrentMonster = null;
        Group mGroup;
        public GroupEditor(Group aGroup)
        {
            mGroup = aGroup;

            InitializeComponent();

            RefreshEnemies();

            this.GameHandle.Paint += new System.Windows.Forms.PaintEventHandler(this.GameHandle_Paint);
            this.GameHandle.MouseDown += new MouseEventHandler(this.GameHandle_MouseDown);
            this.GameHandle.MouseMove += new MouseEventHandler(this.GameHandle_MouseMove);
            this.GameHandle.MouseUp += new MouseEventHandler(this.GameHandle_MouseUp);
        }
        private void GameHandle_MouseUp(object sender, MouseEventArgs e)
        {
            mCurrentMonster = null;
        }
        private void GameHandle_MouseDown(object sender, MouseEventArgs e)
        {
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(0, 0, GameHandle.Width, GameHandle.Height);

            Vector2 v = toData(new Vector2(e.X, e.Y), rect);
            foreach (Monster m in mGroup.Monsters.Keys)
            {
                Vector2 pos = mGroup.Monsters[m];

                Microsoft.Xna.Framework.Rectangle rect1 = new Microsoft.Xna.Framework.Rectangle((int)(v.X - 1.0f), (int)(v.Y - 1.0f), 3, 3);
                Microsoft.Xna.Framework.Rectangle rect2 = new Microsoft.Xna.Framework.Rectangle((int)(pos.X - 1.0f),(int) (pos.Y - 1.0f), 3, 3);

                if (rect1.Intersects(rect2))
                {
                    mCurrentMonster = m;
                }
            }
        }

        private void GameHandle_MouseMove(object sender, MouseEventArgs e)
        {
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(0, 0, GameHandle.Width, GameHandle.Height);

            Vector2 v = toData(new Vector2(e.X, e.Y), rect);

            if (mCurrentMonster != null)
            {
                mGroup.Monsters[mCurrentMonster] = v;

                this.Refresh();
            }
        }

        private Vector2 toScreen(Vector2 pos, Microsoft.Xna.Framework.Rectangle screenRect)
        {
            Vector2 vect = new Vector2();

            vect.X = (screenRect.Width / 2.0f) + ((pos.X) / 100.0f * (screenRect.Width / 2.0f));
            vect.Y = (screenRect.Height / 2.0f) + ((pos.Y) / 100.0f * (screenRect.Height / 2.0f));

            return vect;
        }

        private Vector2 toData(Vector2 pos, Microsoft.Xna.Framework.Rectangle screenRect)
        {
            Vector2 vect = new Vector2();

            vect.X = ((pos.X - (screenRect.Width / 2.0f)) / (screenRect.Width / 2.0f))* 100.0f;
            vect.Y = ((pos.Y - (screenRect.Height / 2.0f)) / (screenRect.Height / 2.0f)) * 100.0f;

            return vect;
        }

        protected void GameHandle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle(0, 0, GameHandle.Width, GameHandle.Height);


            Vector2 v = toScreen(new Vector2(),rect);
            Brush pn = new SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Rectangle prect = new System.Drawing.Rectangle((int)v.X-3, (int)v.Y-3, 7, 7);
            g.FillEllipse(pn, prect); 

            foreach (Monster m in mGroup.Monsters.Keys)
            {
                Vector2 vect = mGroup.Monsters[m];
                Vector2 toPrint = toScreen(vect, rect);

                pn = new SolidBrush(System.Drawing.Color.Blue);
                prect = new System.Drawing.Rectangle((int)toPrint.X - 5, (int)toPrint.Y - 5, 11, 11);
                g.FillEllipse(pn, prect); 
            }
        }

        private void GameHandle_Click(object sender, EventArgs e)
        {

        }

        private void GroupEditor_Load(object sender, EventArgs e)
        {

        }
        private void RefreshEnemies()
        {
            EnemiesListView.Nodes.Clear();
            foreach (Monster m in mGroup.MonstersOrder)
            {
                TreeNode enemies = new TreeNode(m.ToString());
                EnemiesListView.Nodes.Add(enemies);
            }
           
        }

        private List<Monster> GetSelectedMonsters()
        {
            List<Monster> list = new List<Monster>();
            TreeNode item = EnemiesListView.SelectedNode;
            char[] sep = new char[1];
            sep[0] = ' ';
            if (!item.Text.StartsWith("Monster"))
            {
                return new List<Monster>();
            }
            int id = Convert.ToInt32(item.Text.Split(sep)[1]);

            List<Monster> toDelete = new List<Monster>();
            foreach (Monster m in mGroup.MonstersOrder)
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

            return list;
        }

        private void Up_Click(object sender, EventArgs e)
        {
            List<Monster> monster = GetSelectedMonsters();
            int i = mGroup.MonstersOrder.IndexOf(monster[0]);

            if (i > 0)
            {
                Monster m = mGroup.MonstersOrder[i - 1];
                mGroup.MonstersOrder[i - 1] = mGroup.MonstersOrder[i]; ;
                mGroup.MonstersOrder[i] = m;
            }

            RefreshEnemies();
        }

        private void Down_Click(object sender, EventArgs e)
        {
            List<Monster> monster = GetSelectedMonsters();
            int i = mGroup.MonstersOrder.IndexOf(monster[0]);

            if (i < mGroup.MonstersOrder.Count - 1)
            {
                Monster m = mGroup.MonstersOrder[i + 1];
                mGroup.MonstersOrder[i + 1] = mGroup.MonstersOrder[i]; ;
                mGroup.MonstersOrder[i] = m;
            }

            RefreshEnemies();
        }
    }
}
