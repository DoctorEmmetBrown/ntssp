using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace AnimationEngine
{
    public class Engine
    {
        Dictionary<Group,GroupEffect> mEffects = new Dictionary<Group,GroupEffect>();
        List<Group> mGroups = new List<Group>();
        List<Group> mCurrentGroups = new List<Group>();
        bool mChange = true;
        public Engine(Formation aForm)
        {
            mGroups.AddRange(aForm.Groups);
        }
        bool IsDependent(Group a, Group b)
        {
            foreach (Monster e in a.Monsters.Keys)
            {
                if (b.Monsters.Keys.Contains(e))
                    return true;
            }
            return false;
        }
        List<Group> GetCurrentGroups()
        {
            List<Group> groups = new List<Group>();
            groups.AddRange(mGroups);
            int i = 0;
            while (i < groups.Count)
            {
                int j = i + 1;
                while (j < groups.Count)
                {
                    if (IsDependent(groups[i], groups[j]))
                    {
                        groups.Remove(groups[j]);
                    }
                    else
                    {
                        j = j + 1;
                    }
                }
                i = i + 1;
            }
            return groups;
        }

        public bool GlobalAnimation(GameTime aGameTime)
        {
            if(mChange)
            {
                mCurrentGroups = GetCurrentGroups();
                mChange = false;
            }

            foreach(Group g in mCurrentGroups)
            {
                AnimateGroup(g,aGameTime);
            }

            if(mCurrentGroups.Count == 0)
            {
                return false;
            }
            return true;
        }

        void EndGroup(GroupEffect aGroupEffect)
        {
            mChange = true;
            mGroups.Remove(aGroupEffect.Group);
        }
        void EndEnemy(GroupEffect aGroupEffect)
        {
            mChange = true;
        }
        
        void  AnimateGroup(Group aGroup, GameTime aGameTime)
        {
            if(!mEffects.Keys.Contains(aGroup))
            {
                mEffects.Add(aGroup,GroupEffect.GetEffect(aGroup));
                mEffects[aGroup].End = this.EndGroup;
                mEffects[aGroup].EndEnemy = this.EndEnemy;
            }

            GroupEffect e = mEffects[aGroup];
            e.Animate(aGameTime);
        }
    }
}
