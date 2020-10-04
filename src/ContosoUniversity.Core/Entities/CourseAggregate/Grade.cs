using Ardalis.SmartEnum;

namespace ContosoUniversity.Core.Entities.CourseAggregate
{
    public abstract class Grade : SmartEnum<Grade>
    {
        public static readonly Grade A = new AGrade();
        public static readonly Grade AMinus = new AMinusGrade();
        public static readonly Grade BPlus = new BPlusGrade();
        public static readonly Grade B = new BGrade();
        public static readonly Grade BMinus = new BMinusGrade();
        public static readonly Grade CPlus = new CPlusGrade();
        public static readonly Grade C = new CGrade();
        public static readonly Grade CMinus = new CMinusGrade();
        public static readonly Grade DPlus = new DPlusGrade();
        public static readonly Grade D = new DGrade();
        public static readonly Grade DMinus = new DMinusGrade();
        public static readonly Grade F = new FGrade();

        protected Grade(int id, string name) : base(name, id)
        {
        }

        public abstract double QualityPoints { get; }

        private class AGrade : Grade
        {
            public AGrade() : base(1, "A")
            {
            }

            public override double QualityPoints => 4;
        }

        private class AMinusGrade : Grade
        {
            public AMinusGrade() : base(2, "A-")
            {
            }

            public override double QualityPoints => 3.67;
        }

        private class BPlusGrade : Grade
        {
            public BPlusGrade() : base(3, "B+")
            {
            }

            public override double QualityPoints => 3.33;
        }

        private class BGrade : Grade
        {
            public BGrade() : base(4, "B")
            {
            }

            public override double QualityPoints => 3;
        }

        private class BMinusGrade : Grade
        {
            public BMinusGrade() : base(5, "B-")
            {
            }

            public override double QualityPoints => 2.67;
        }

        private class CPlusGrade : Grade
        {
            public CPlusGrade() : base(6, "C+")
            {
            }

            public override double QualityPoints => 2.33;
        }

        private class CGrade : Grade
        {
            public CGrade() : base(7, "C")
            {
            }

            public override double QualityPoints => 2;
        }

        private class CMinusGrade : Grade
        {
            public CMinusGrade() : base(8, "C-")
            {
            }

            public override double QualityPoints => 1.67;
        }

        private class DPlusGrade : Grade
        {
            public DPlusGrade() : base(9, "D+")
            {
            }

            public override double QualityPoints => 1.33;
        }

        private class DGrade : Grade
        {
            public DGrade() : base(10, "D")
            {
            }

            public override double QualityPoints => 1.00;
        }

        private class DMinusGrade : Grade
        {
            public DMinusGrade() : base(11, "D-")
            {
            }

            public override double QualityPoints => 0.67;
        }

        private class FGrade : Grade
        {
            public FGrade() : base(12, "F")
            {
            }

            public override double QualityPoints => 0;
        }
    }
}