﻿using BLL.Entities.BaseEntities;

namespace BLL.Entities
{
    public class FriendUser : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int FriendListId { get; set; }
        public FriendList FriendList { get; set; } = null!;
    }
}
