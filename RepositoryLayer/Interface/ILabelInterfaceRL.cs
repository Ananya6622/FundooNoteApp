using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelInterfaceRL
    {
        public LabelEntity AddLabel(LabelModel labelModel, long noteId, long userId);
        public List<LabelEntity> GetAllLabels(long labelId, long UserId, long noteId);
        public bool UpdateLabel(long labelId, long noteId, long userId, LabelModel labelModel);
        public bool DeleteLabel(long labelId, long noteId, long userId);
    }
}
