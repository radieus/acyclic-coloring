using System;

public class Edge : IComparable<Edge> , IEquatable<Edge>
{
    public int a;
    public int b;
    public Edge(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
    public int CompareTo(Edge otherEdge)
    {
        if (otherEdge == null)
            return 1;

        if (this.a == otherEdge.a && this.b == otherEdge.b)
            return 0;
        if (this.a == otherEdge.b && this.b == otherEdge.a)
            return 0;
            
        return 1;
    }

    public override bool Equals(object obj) => this.Equals(obj as Edge);

        public bool Equals(Edge p)
        {
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            if (this.a == p.a && this.b == p.b)
                return true;
            if (this.a == p.b && this.b == p.a)
                return true;
            
            return false;
        }

        public override int GetHashCode() => (a, b).GetHashCode();
        public static bool operator ==(Edge lhs, Edge rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Edge lhs, Edge rhs) => !(lhs == rhs);
}