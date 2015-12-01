[1mdiff --git a/MySensei/.vs/MySensei/v14/.suo b/MySensei/.vs/MySensei/v14/.suo[m
[1mindex fc86ed7..7c6b1e1 100644[m
Binary files a/MySensei/.vs/MySensei/v14/.suo and b/MySensei/.vs/MySensei/v14/.suo differ
[1mdiff --git a/MySensei/MySensei/App_Data/MySenseiDb.mdf b/MySensei/MySensei/App_Data/MySenseiDb.mdf[m
[1mindex 449fe13..d448add 100644[m
Binary files a/MySensei/MySensei/App_Data/MySenseiDb.mdf and b/MySensei/MySensei/App_Data/MySenseiDb.mdf differ
[1mdiff --git a/MySensei/MySensei/App_Data/MySenseiDb_log.ldf b/MySensei/MySensei/App_Data/MySenseiDb_log.ldf[m
[1mindex b2331f7..1164361 100644[m
Binary files a/MySensei/MySensei/App_Data/MySenseiDb_log.ldf and b/MySensei/MySensei/App_Data/MySenseiDb_log.ldf differ
[1mdiff --git a/MySensei/MySensei/Controllers/HomeController.cs b/MySensei/MySensei/Controllers/HomeController.cs[m
[1mindex ec925f8..a782aa9 100644[m
[1m--- a/MySensei/MySensei/Controllers/HomeController.cs[m
[1m+++ b/MySensei/MySensei/Controllers/HomeController.cs[m
[36m@@ -38,5 +38,6 @@[m [mnamespace MySensei.Controllers[m
             dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));[m
             return dict;[m
         }[m
[31m-    }[m
[31m-}[m
\ No newline at end of file[m
[32m+[m
[32m+[m[32m        }[m
[32m+[m[32m    }[m
\ No newline at end of file[m
[1mdiff --git a/MySensei/MySensei/Views/Admin/Index.cshtml b/MySensei/MySensei/Views/Admin/Index.cshtml[m
[1mindex c9f5a7d..e9a4080 100644[m
[1m--- a/MySensei/MySensei/Views/Admin/Index.cshtml[m
[1m+++ b/MySensei/MySensei/Views/Admin/Index.cshtml[m
[36m@@ -7,6 +7,7 @@[m
     <div class="panel-heading">[m
         User Accounts[m
     </div>[m
[32m+[m
     <table class="table table-striped">[m
         <tr><th>ID</th><th>User name</th><th>Email</th><th></th></tr>[m
         @if (Model.Count() == 0)[m
[36m@@ -32,8 +33,8 @@[m
                         }[m
                     </td>[m
                 </tr>[m
[32m+[m[32m                }[m
             }[m
[31m-        }[m
     </table>[m
 </div>[m
 @Html.ActionLink("Create", "Create", null, new { @class = "btn btn-primary" })[m
\ No newline at end of file[m
[1mdiff --git a/MySensei/MySensei/bin/MySensei.dll b/MySensei/MySensei/bin/MySensei.dll[m
[1mindex e947891..133c730 100644[m
Binary files a/MySensei/MySensei/bin/MySensei.dll and b/MySensei/MySensei/bin/MySensei.dll differ
[1mdiff --git a/MySensei/MySensei/bin/MySensei.pdb b/MySensei/MySensei/bin/MySensei.pdb[m
[1mindex d1770ad..d4d7988 100644[m
Binary files a/MySensei/MySensei/bin/MySensei.pdb and b/MySensei/MySensei/bin/MySensei.pdb differ
[1mdiff --git a/MySensei/MySensei/obj/Debug/MySensei.dll b/MySensei/MySensei/obj/Debug/MySensei.dll[m
[1mindex e947891..133c730 100644[m
Binary files a/MySensei/MySensei/obj/Debug/MySensei.dll and b/MySensei/MySensei/obj/Debug/MySensei.dll differ
[1mdiff --git a/MySensei/MySensei/obj/Debug/MySensei.pdb b/MySensei/MySensei/obj/Debug/MySensei.pdb[m
[1mindex d1770ad..d4d7988 100644[m
Binary files a/MySensei/MySensei/obj/Debug/MySensei.pdb and b/MySensei/MySensei/obj/Debug/MySensei.pdb differ
