﻿using ColoradoBeetle.Application.GroupProducts.Commands.AddGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.AddMultiGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.CheckGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.CheckStockGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.CopyAllGroupProducts;
using ColoradoBeetle.Application.GroupProducts.Commands.CopyOneGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.DeleteGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Commands.EditGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Queries.GetChildGroupProducts;
using ColoradoBeetle.Application.GroupProducts.Queries.GetEditGroupProduct;
using ColoradoBeetle.Application.GroupProducts.Queries.GetGroupProducts;
using ColoradoBeetle.Application.Products.Commands.CheckProduct;
using ColoradoBeetle.Application.Products.Commands.CheckStockProduct;
using ColoradoBeetle.Application.Products.Commands.CopyOneProduct;
using ColoradoBeetle.Application.Products.Queries.GetChildProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ColoradoBeetle.UI.Controllers; 

[Authorize]
public class GroupProductController : BaseController {
    private readonly ILogger<GroupProductController> _logger;

    public GroupProductController(ILogger<GroupProductController> logger)
    {
        _logger = logger;
    }
    public async Task<IActionResult> GroupProducts(int id, int groupId) {
        return View(await Mediator.Send(new GetGroupProductsQuery
        {
            GroupShopListId = id,
            GroupId = groupId,
            UserId = UserId
        }));
    }

    public IActionResult AddGroupProduct(int shopListId, int groupId) {
        return View(new AddGroupProductCommand {
            GroupShopListId = shopListId,
            GroupId = groupId,
            UserId = UserId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGroupProduct(AddGroupProductCommand command) {

        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Produkt został dodany";

        return RedirectToAction("GroupProducts", "GroupProduct",
            new { id = command.GroupShopListId, groupId = command.GroupId });
    }

    public async Task<IActionResult> EditGroupProduct(int id, int groupId) {
        return View(await Mediator.Send(new GetEditGroupProductQuery {
            Id = id,
            GroupId = groupId,
            UserId = UserId
        }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditGroupProduct(EditGroupProductCommand command) {

        var result  = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Produkt został zaktualizowany";

        return RedirectToAction("GroupProducts", "GroupProduct",
            new { id = command.GroupShopListId, groupId = command.GroupId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteGroupProduct(int id) {

        try {
            await Mediator.Send(new DeleteGroupProductCommand {
                Id = id,
                UserId = UserId,
            });

            return Json( new { success = true });
        }
        catch (Exception exception){
            _logger.LogError(exception, null);

            return Json( new { success = false });
        }
    }

    public async Task<IActionResult> CopyAllGroupProducts(int childId, int prntId, 
        int groupId) {

        try {
            await Mediator.Send(new CopyAllGroupProductsCommand {
                PrntId = prntId,
                ChildId = childId,
                GroupId = groupId,
                UserId = UserId
            });

            TempData["Success"] = "Wszystkie produkty zostały skopiowane";

            return RedirectToAction("GroupProducts", "GroupProduct", 
                new { id = prntId, groupId });
        }
        catch (Exception exception) {
            _logger.LogError(exception, null);
            TempData["Error"] = "Kopiowanie nieudane, " +
                "spróbuj ponownie lub skotaktuj się administratorem";
            return RedirectToAction("GroupProducts", "GroupProduct",
                new { id = prntId, groupId });

        }
    }

    public async Task<IActionResult> ChildGroupProducts(int id, int prntId, int groupId) {

        return View(await Mediator.Send(new GetChildGroupProductsQuery {
            ChildId = id,
            PrntId = prntId,
            UserId = UserId,
            GroupId = groupId
        }));
    }

    [HttpPost]
    public async Task<IActionResult> CopyOneGroupProduct(int id, int prntId) {

        try {
            await Mediator.Send(new CopyOneGroupProductCommand {
                Id = id,
                PrntId = prntId,
                UserId = UserId
            });

            return Json(new { success = true });
        }
        catch (Exception exception) {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CheckGroupProduct(int id, bool check) {

        try {
            await Mediator.Send(new CheckGroupProductCommand {
                Id = id,
                IsChecked = check,
                UserId = UserId
            });
            return Json(new { success = true });
        }
        catch (Exception exception) {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CheckStockGroupProduct(int id, bool onStock) {

        try {
            await Mediator.Send(new CheckStockGroupProductCommand {
                Id = id,
                OnStock = onStock,
                UserId = UserId
            });
            return Json(new { success = true });
        }
        catch (Exception exception) {
            _logger.LogError(exception, null);
            return Json(new { success = false });
        }
    }

    public IActionResult AddMultiGroupProduct(int shopListId, int groupId) {
        return View(new AddMultiGroupProductCommand {
            GroupShopListId = shopListId,
            GroupId = groupId,
            UserId = UserId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMultiGroupProduct(AddMultiGroupProductCommand command) {

        var result = await MediatorSendValidate(command);

        if (!result.IsValid)
            return View(command);

        TempData["Success"] = "Produkty zostały dodane";

        return RedirectToAction("GroupProducts", "GroupProduct",
            new { id = command.GroupShopListId, groupId = command.GroupId });
    }
}
