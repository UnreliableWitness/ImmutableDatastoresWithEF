using System;
using System.Collections.Generic;
using System.Linq;

namespace AggregateEvent.Models
{
    public class PartRepository
    {
        public List<Part> GetAll()
        {
            using (var db = new StockContext())
            {
                return db.Parts.ToList();    
            }
        }

        public void Buy(int partId, double amount, string transactionId)
        {
            using (var db = new StockContext())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //enforce idempotence -> if transactionid exists -> exit
                        if (db.Deliveries.Any(d => d.TransactionId == transactionId))
                            return;

                        //adding the "event" entity
                        db.Deliveries.Add(
                            new Purchase 
                        {
                            TransactionId = transactionId,
                            Amount = amount, 
                            PartId = partId, 
                            TimeOfPurchase = DateTime.UtcNow
                        });

                        var currentPartStock = db.PartStocks.SingleOrDefault(p => p.PartId == partId);
                        
                        //and updating the "aggregate" entity
                        if (currentPartStock != null)
                        {
                            currentPartStock.TotalBought += amount;
                            currentPartStock.InStock += amount;
                        }
                        else
                        {
                            currentPartStock = new PartStock
                            {
                                PartId = partId,
                                InStock = amount,
                                TotalBought = amount
                            };

                            db.PartStocks.Add(currentPartStock);
                        }


                        db.SaveChanges();
                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        //should things go south, all changes are rolled back and the message can safely be submitted again
                        dbTransaction.Rollback();
                    }
                }
            }


        }

        public void Sell(int partId, double amount, string transactionId)
        {
            using (var db = new StockContext())
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //enforce idempotence -> if transactionid exists -> exit
                        if (db.Sales.Any(d => d.TransactionId == transactionId))
                            return;

                        //adding the "event" entity
                        db.Sales.Add(new Sale
                        {
                            TransactionId = transactionId,
                            PartId = partId,
                            Quantity = amount,
                            TimeOfSale = DateTime.UtcNow
                        });

                         var currentPartStock = db.PartStocks.SingleOrDefault(p => p.PartId == partId);
                        
                        //updating the "aggregate" entity
                        if (currentPartStock != null)
                        {
                            currentPartStock.TotalSold += amount;
                            currentPartStock.InStock -= amount;
                        }
                        else
                        {
                            db.PartStocks.Add(new PartStock
                            {
                                PartId = partId,
                                InStock = -amount,
                                TotalSold = amount
                            });
                        }

                        db.SaveChanges();

                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        //should things go south, all changes are rolled back and the message can safely be submitted again
                        dbTransaction.Rollback();
                    }
                }
            }
        }
    }
}
