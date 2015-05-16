using System;
using AggregateEvent.Models;

namespace AggregateEvent
{
    class Program
    {
        private static PartRepository _partRepository;


        static void Main(string[] args)
        {
            try
            {
                _partRepository = new PartRepository();

                //var parts = _partRepository.GetAll();

                //buying many parts
                for (int i = 0; i < 100; i++)
                {
                    _partRepository.Buy(1, i, string.Format("transaction{0}", i));
                }


                //selling many parts 
                for (int i = 0; i < 100; i++)
                {
                    _partRepository.Sell(1, i, string.Format("transaction{0}",i));

                }

                //this is of course a contrived example because in a real world application buying and selling happens in no particular order


                //do it all again! just to emphasize the idempotency of the whole ordeal
                for (int i = 0; i < 100; i++)
                {
                    _partRepository.Buy(1, i, string.Format("transaction{0}", i));
                }

                for (int i = 0; i < 100; i++)
                {
                    _partRepository.Sell(1, i, string.Format("transaction{0}", i));

                }

                Console.WriteLine("done buying and selling");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }

            Console.WriteLine("press key to quit");
            Console.ReadKey();
        }
    }


}
