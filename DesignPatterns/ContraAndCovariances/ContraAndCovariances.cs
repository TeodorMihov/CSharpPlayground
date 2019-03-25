namespace DesignPatterns.ContraAndCovariances
{
    // TODO: separate in different classes
    class ContraAndCovariances
    {
        static void Run()
        {
            ICovariant<Fruit> apple = new Covariant<Apple>(); //because it's covariant
            IContravariant<Apple> fruit = new Contravariant<Fruit>(); //because it's contravariant
        }

        public interface ICovariant<out T> { }
        public interface IContravariant<in T> { }

        public class Covariant<T> : ICovariant<T> { }
        public class Contravariant<T> : IContravariant<T> { }

        public class Fruit { }
        public class Apple : Fruit { }

        public class TheInsAndOuts
        {
            public void Covariance()
            {
                ICovariant<Fruit> fruit = new Covariant<Fruit>();
                ICovariant<Apple> apple = new Covariant<Apple>();

                Covariant(fruit);
                Covariant(apple); //apple is being upcasted to fruit, without the out keyword this will not compile
            }

            public void Contravariance()
            {
                IContravariant<Fruit> fruit = new Contravariant<Fruit>();
                IContravariant<Apple> apple = new Contravariant<Apple>();

                Contravariant(fruit); //fruit is being downcasted to apple, without the in keyword this will not compile
                Contravariant(apple);
            }

            public void Covariant(ICovariant<Fruit> fruit) { }

            public void Contravariant(IContravariant<Apple> apple) { }
        }
    }
}
