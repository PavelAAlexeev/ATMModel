// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATMModel.Migrations
{
    [DbContext(typeof(ATMModelContext))]
    [Migration("20210728184901_AddedUserCardRelations1")]
    partial class AddedUserCardRelations1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ATMModel.Models.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Blocked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("CountOfWrongTry")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedPin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CardNumber");

                    b.HasIndex("UserId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("ATMModel.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ATMModel.Models.Card", b =>
                {
                    b.HasOne("ATMModel.Models.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ATMModel.Models.User", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
