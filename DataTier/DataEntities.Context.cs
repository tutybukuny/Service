﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataTier
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TheProjectEntities : DbContext
    {
        public TheProjectEntities()
            : base("name=TheProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActionType> ActionTypes { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChatContent> ChatContents { get; set; }
        public virtual DbSet<ChatGroup> ChatGroups { get; set; }
        public virtual DbSet<ChatGroupContent> ChatGroupContents { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Following> Followings { get; set; }
        public virtual DbSet<FollowingProject> FollowingProjects { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<JoinedProject> JoinedProjects { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}