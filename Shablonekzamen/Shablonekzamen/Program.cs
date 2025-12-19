using System;

namespace OnlineShop
{

    public interface IDeliveryMethod
    {
        double CalculateCost();
        string GetStatus();
    }

    public interface IPaymentMethod
    {
        void Pay(double amount);
    }


    public class CourierDelivery : IDeliveryMethod
    {
        public double CalculateCost()
        {
            return 1500;
        }

        public string GetStatus()
        {
            return "Заказ передан курьеру";
        }
    }

    public class PickupDelivery : IDeliveryMethod
    {
        public double CalculateCost()
        {
            return 0;
        }

        public string GetStatus()
        {
            return "Заказ готов к получению";
        }
    }

    public class PostalDelivery : IDeliveryMethod
    {
        public double CalculateCost()
        {
            return 2500;
        }

        public string GetStatus()
        {
            return "Заказ отправлен почтой";
        }
    }


    public class CardPayment : IPaymentMethod
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплата картой");
            Console.WriteLine("Сумма: " + amount);
        }
    }

    public class CashPayment : IPaymentMethod
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Оплата наличными при получении");
            Console.WriteLine("Сумма: " + amount);
        }
    }

    public class OnlinePayment : IPaymentMethod
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Онлайн-оплата через сервис");
            Console.WriteLine("Сумма: " + amount);
            Console.WriteLine("Платеж подтвержден");
        }
    }


    public interface IOrderFactory
    {
        IDeliveryMethod CreateDelivery();
        IPaymentMethod CreatePayment();
    }


    public class CourierOrderFactory : IOrderFactory
    {
        public IDeliveryMethod CreateDelivery()
        {
            return new CourierDelivery();
        }

        public IPaymentMethod CreatePayment()
        {
            return new CardPayment();
        }
    }

    public class PickupOrderFactory : IOrderFactory
    {
        public IDeliveryMethod CreateDelivery()
        {
            return new PickupDelivery();
        }

        public IPaymentMethod CreatePayment()
        {
            return new CashPayment();
        }
    }

    public class PostalOrderFactory : IOrderFactory
    {
        public IDeliveryMethod CreateDelivery()
        {
            return new PostalDelivery();
        }

        public IPaymentMethod CreatePayment()
        {
            return new OnlinePayment();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите способ доставки:");
            Console.WriteLine("1 - Курьер");
            Console.WriteLine("2 - Самовывоз");
            Console.WriteLine("3 - Почта");

            int choice = int.Parse(Console.ReadLine());

            IOrderFactory factory;

            if (choice == 1)
                factory = new CourierOrderFactory();
            else if (choice == 2)
                factory = new PickupOrderFactory();
            else
                factory = new PostalOrderFactory();

            IDeliveryMethod delivery = factory.CreateDelivery();
            IPaymentMethod payment = factory.CreatePayment();

            double orderPrice = 10000;
            double deliveryCost = delivery.CalculateCost();
            double totalPrice = orderPrice + deliveryCost;

            Console.WriteLine("Стоимость товара: " + orderPrice);
            Console.WriteLine("Стоимость доставки: " + deliveryCost);
            Console.WriteLine("Итого к оплате: " + totalPrice);
            Console.WriteLine("Статус доставки: " + delivery.GetStatus());

            payment.Pay(totalPrice);

            Console.ReadLine();
        }
    }
}
