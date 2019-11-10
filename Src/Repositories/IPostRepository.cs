﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Business;

namespace Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> List(int forumId);

        Task<Post> Read(int id);

        Task<Post> Create(Post post);

        Task Update(Post post);

        Task Delete(Post post);
    }
}