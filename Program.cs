namespace Structural.Composite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Booster boosterBundle = new CompositeBooster(3);
            Booster damageBooster = new DamageBooster(5);
            Booster strengthBooster = new StrengthBooster(3);
            Booster magicBooster = new MagicBooster(5);

            boosterBundle.Add(magicBooster);
            boosterBundle.Add(damageBooster);
            boosterBundle.Add(strengthBooster);

            Console.WriteLine(boosterBundle.GetBoostAsString());

            Console.WriteLine("Single boosts");
            damageBooster.Add(magicBooster);
            Console.WriteLine(damageBooster.GetBoostAsString());
            Console.WriteLine(magicBooster.GetBoostAsString());
            Console.WriteLine(strengthBooster.GetBoostAsString());

            Console.ReadKey();
        }
    }

    public abstract class Booster // Used to be an interface, but the AsString Method appeared
    {
        public abstract List<Tuple<string, int>> GetBoost();
        public abstract bool Add(Booster c);

        public string GetBoostAsString()
        {
            List<Tuple<string, int>> boosts = GetBoost();
            string boostsAsString = "";
            foreach (Tuple<string, int> boost in boosts)
            {
                boostsAsString += boost.Item1 + " +" + boost.Item2 + "\n";
            }

            return boostsAsString;
        }
    }

    public abstract class SimpleBooster : Booster // Made for DRY purposes
    {
        public override bool Add(Booster c)
        {
            Console.WriteLine("Cannot add booster to simple booster");
            return false;
        }
    }

    public class DamageBooster : SimpleBooster
    {
        private static readonly string STAT_KEY = "damage";
        private List<Tuple<string, int>> boost;

        public DamageBooster(int value)
        {
            this.boost = new List<Tuple<string, int>>
            {
                new Tuple<string, int>(STAT_KEY, value)
            }; //weird
        }
        public override List<Tuple<string, int>> GetBoost()
        {
            return this.boost;
        }

        public override bool Add(Booster c)
        {
            Console.WriteLine("Cannot add to simple booster");
            return false;
        }
    }

    public class StrengthBooster : SimpleBooster
    {
        private static readonly string STAT_KEY = "strength";
        private List<Tuple<string, int>> boost;

        public StrengthBooster(int value)
        {
            this.boost = new List<Tuple<string, int>>
            {
                new Tuple<string, int>(STAT_KEY, value)
            };
        }
        public override List<Tuple<string, int>> GetBoost()
        {
            return this.boost;
        }
    }

    public class MagicBooster : SimpleBooster
    {
        private static readonly string STAT_KEY = "magic";
        private List<Tuple<string, int>> boost;

        public MagicBooster(int value)
        {
            this.boost = new List<Tuple<string, int>>
            {
                new Tuple<string, int>(STAT_KEY, value)
            };
        }
        public override List<Tuple<string, int>> GetBoost()
        {
            return this.boost;
        }
    }

    public class CompositeBooster: Booster
    {
        public readonly int compositeLimit;
        protected List<Booster> boosters;

        public CompositeBooster(int limit)
        {
            this.compositeLimit = limit;
            boosters = new List<Booster>(limit);
        }

        public override bool Add(Booster booster)
        {
            if (boosters.Count == this.compositeLimit) {
                return false;
            }
            boosters.Add(booster);
            return true;
        }

        public override List<Tuple<string, int>> GetBoost()
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();
            foreach (Booster boost in boosters) {
                result.AddRange(boost.GetBoost());
            }
            return result;
        }
    }
    /*
    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    public abstract class Component
    {
        protected string name;
        // Constructor
        public Component(string name)
        {
            this.name = name;
        }
        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }
    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    public class Leaf : Component
    {
        // Constructor
        public Leaf(string name) : base(name)
        {
        }
        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }
        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
        }
    }
    /// <summary>
    /// The 'Composite' class
    /// </summary>
    public class Composite : Component
    {
        List<Component> children = new List<Component>();
        // Constructor
        public Composite(string name) : base(name)
        {
        }
        public override void Add(Component component)
        {
            children.Add(component);
        }
        public override void Remove(Component component)
        {
            children.Remove(component);
        }
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);
            // Recursively display child nodes
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }
    }*/
}
