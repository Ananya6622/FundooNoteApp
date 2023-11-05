using BusinessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL:ILabelBL
    {
        private readonly ILabelInterfaceRL labelInterfaceRL;

        public LabelBL(ILabelInterfaceRL labelInterfaceRL)
        {
            this.labelInterfaceRL = labelInterfaceRL;
        }

        public LabelEntity AddLabel(LabelModel labelModel, long noteId, long userId)
        {
            try
            {
                return this.labelInterfaceRL.AddLabel(labelModel,noteId,userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelEntity> GetAllLabels(long labelId, long UserId, long noteId)
        {
            try
            {
                return this.labelInterfaceRL.GetAllLabels(labelId, UserId, noteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateLabel(long labelId, long noteId, long userId, LabelModel labelModel)
        {
            try
            {
                return this.labelInterfaceRL.UpdateLabel(noteId, userId,labelId, labelModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long labelId, long noteId, long userId)
        {
            try
            {
                return this.labelInterfaceRL.DeleteLabel(labelId, noteId, userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
