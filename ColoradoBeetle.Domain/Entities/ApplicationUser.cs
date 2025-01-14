﻿using Microsoft.AspNetCore.Identity;

namespace ColoradoBeetle.Domain.Entities;
public class ApplicationUser : IdentityUser{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime RegisterDateTime { get; set; }
    public bool IsDeleted { get; set; }


    public Address Address { get; set; }
    public Client Client { get; set; }
    public ICollection<ShoppingList> ShoppingLists { get; set; } = new HashSet<ShoppingList>();
    public ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<GroupShopList> GroupShopLists { get; set; } 
        = new HashSet<GroupShopList>();
    public ICollection<GroupShopList> EditedGroupShopLists { get; set; }
        = new HashSet<GroupShopList>();
    public ICollection<GroupProduct> GroupProducts { get; set; } = new HashSet<GroupProduct>();
    public ICollection<GroupProduct> EditedGroupProducts { get; set; }
        = new HashSet<GroupProduct>();

}
