using Microsoft.EntityFrameworkCore;
using FromulaOne.Entities.DbSet;

namespace FromulaOne.DataService.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<Achievement> Achievements { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 首先调用基类的 OnModelCreating 方法，以确保任何基类中的配置都被应用
        base.OnModelCreating(modelBuilder);

        // 配置 Achievement 实体。 entity 代表 Achievement 实体的配置构建器。
        modelBuilder.Entity<Achievement>(entity =>
        {
            // 有一个
            entity.HasOne(d => d.Deiver)
                // 可以有多个
                .WithMany(p => p.Achievements)
                // 外键
                .HasForeignKey(d => d.DriverId)
                // 在 Driver 被删除时，不会级联删除相关的 Achievement 记录
                .OnDelete(DeleteBehavior.NoAction)
                // 设置外键约束的名称
                .HasConstraintName("FK_Achievements_Driver");
        });
    }
}
