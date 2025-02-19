﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.BookStore;

public static class BookStoreModuleExtensionConfigurator
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ConfigureExistingProperties();
            ConfigureExtraProperties();
        });
    }

    private static void ConfigureExistingProperties()
    {
        /* You can change max lengths for properties of the
         * entities defined in the modules used by your application.
         *
         * Example: Change user and role name max lengths

           AbpUserConsts.MaxNameLength = 99;
           IdentityRoleConsts.MaxNameLength = 99;

         * Notice: It is not suggested to change property lengths
         * unless you really need it. Go with the standard values wherever possible.
         *
         * If you are using EF Core, you will need to run the add-migration command after your changes.
         */
    }

    public static void ConfigureExtraProperties()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance.Modules()
                .ConfigureCmsKit(cmsKit =>
                {
                    cmsKit.ConfigureBlog(plan => // extend the Blog entity
                    {
                        plan.AddOrUpdateProperty<string>( //property type: string
                          "BlogDescription", //property name
                          property => {
                              //validation rules
                              property.Attributes.Add(new RequiredAttribute()); //adds required attribute to the defined property

                              //...other configurations for this property
                          }
                        );
                    });

                    cmsKit.ConfigureBlogPost(blogPost => // extend the BlogPost entity
                    {
                        blogPost.AddOrUpdateProperty<string>( //property type: string
                    "BlogPostDescription", //property name
                    property => {
                        //validation rules
                                property.Attributes.Add(new RequiredAttribute()); //adds required attribute to the defined property

                        //...other configurations for this property
                            }
                    );
                    });
                });
        });
    }

}
