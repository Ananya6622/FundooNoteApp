using BusinessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabInterfaceRL collabInterfaceRL;
        public CollabBL(ICollabInterfaceRL collabInterfaceRL)
        {
            this.collabInterfaceRL = collabInterfaceRL;
        }

        public CollabEntity AddCollab(CollabModel collabModel, long noteId, long userId)
        {
            try
            {
                return collabInterfaceRL.AddCollab(collabModel, noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabEntity> GetAllCollabs()
        {
            try
            {
                return collabInterfaceRL.GetAllCollabs();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteCollab(long collabId, long noteId, long userId)
        {
            try
            {
                return collabInterfaceRL.DeleteCollab(collabId, noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
