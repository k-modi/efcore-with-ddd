using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using efcore2_webapi.Repository.Mappings;

namespace efcore2webapi.Migrations
{
    [DbContext(typeof(ModelBuilderContext))]
    partial class ModelBuilderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("efcore2_webapi.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CommentedOn");

                    b.Property<int>("CommenterId");

                    b.Property<string>("Text");

                    b.Property<int?>("TodoItemId");

                    b.HasKey("Id");

                    b.HasIndex("TodoItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("efcore2_webapi.Domain.Entities.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DueDate");

                    b.Property<int?>("OwnerId");

                    b.Property<string>("Summary");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("efcore2_webapi.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("efcore2_webapi.Domain.Entities.Comment", b =>
                {
                    b.HasOne("efcore2_webapi.Domain.Entities.TodoItem", "TodoItem")
                        .WithMany("Comments")
                        .HasForeignKey("TodoItemId");
                });
        }
    }
}
