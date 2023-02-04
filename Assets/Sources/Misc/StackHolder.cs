using Assets.Sources.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Sources.Misc
{
    /// <summary>
    /// Utility class to handle different types in a stack
    /// </summary>
    public class StackHolder
    {
        public List<Follower> Followers;
        public List<Resource> Resources;
        public List<Location> Locations;
        public List<PNJ> Pnjs;

        public List<Card> UselessCards = new List<Card>();

        public StackHolder(List<Card> stack, List<Type> allowedTypes)
        {
            Followers = stack.OfType<Follower>().ToList();
            Resources = stack.OfType<Resource>().ToList();
            Locations = stack.OfType<Location>().ToList();
            Pnjs = stack.OfType<PNJ>().ToList();

            //Les cartes de types qui n'interragiront pas
            //UselessCards = stack.Where(c => c is not Follower && c is not Resource).ToList();
            if (allowedTypes != null)
                UselessCards = stack.Where(e => allowedTypes.All(t => !t.IsAssignableFrom(e.GetType()))).ToList();
        }

        public bool HasUselessCards()
        {
            return UselessCards.Any();
        }
    }
}
