using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabInterfaceRL
    {
        public CollabEntity AddCollab(CollabModel collabModel, long noteId, long userId);
        public List<CollabEntity> GetAllCollabs();
        public bool DeleteCollab(long collabId, long noteId, long userId);
    }
}
