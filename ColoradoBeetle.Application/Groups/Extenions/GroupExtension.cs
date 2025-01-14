﻿using ColoradoBeetle.Application.Groups.Queries.GetGroups;
using ColoradoBeetle.Domain.Entities;

namespace ColoradoBeetle.Application.Groups.Extenions; 
public static class GroupExtension {

    public static GroupDto ToDto(this Group group) {

        if (group == null)
            return null;

        return new GroupDto 
            { Id = group.Id, 
              Name = group.Name 
            };
    }
}
