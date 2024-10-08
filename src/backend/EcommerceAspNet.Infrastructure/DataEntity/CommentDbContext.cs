﻿using EcommerceAspNet.Domain.Entitie.Ecommerce;
using EcommerceAspNet.Domain.Repository.Comment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class CommentDbContext : ICommentReadOnlyRepository, ICommentWriteOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;

        public CommentDbContext(ProjectDbContext dbContext) => _dbContext = dbContext;

        public void Add(CommentEntitie comment)
        {
            _dbContext.Comments.Add(comment);
        }

        public async Task<IList<CommentEntitie>> CommentByProduct(long? id)
        {
            return await _dbContext.Comments.AsNoTracking().Include(d => d.User).Where(d => d.ProductId == id).ToListAsync();
        }
    }
}
