using Assets.Sources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Sources.Misc
{
    public class StackHolder
    {
        public List<PNJ> Pnjs;
        public List<Follower> Followers;
        public List<Resource> Resources;

        public List<Card> UselessCards;

        public StackHolder(List<Card> stack, List<Type> allowedTypes)
        {
            //Determine what kind of action according to what is snapped
            Followers = stack.OfType<Follower>().ToList();
            Resources = stack.OfType<Resource>().ToList();

            //Les cartes de types qui n'interragiront pas
            //UselessCards = stack.Where(c => c is not Follower && c is not Resource).ToList();
            if (allowedTypes != null)
                UselessCards = stack.Where(e => allowedTypes.All(t => !t.IsAssignableFrom(e.GetType()))).ToList();
        }
    }
}
