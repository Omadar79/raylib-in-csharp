using my_game.Enemies;
using my_game.player;
using my_game.projectiles;
using Raylib_cs;

namespace my_game.systems;

public class CollisionSystem
{
    public void UpdateCollisions( Span<Enemy> enemies, Span<Projectile> projectiles, Player player)
    {
        //Loop through all projectiles
        for (int i= 0; i < projectiles.Length; i++)
        {
            ref Projectile projectile = ref projectiles[i];
            if (!projectile.IsActive) continue;

            if (projectile.ProjectileSource == ProjectileSource.Player ||
                projectile.ProjectileSource == ProjectileSource.All)
            {
                // Check collision with each enemy
                for (int j = 0; j < enemies.Length; j++)
                {
                    ref Enemy enemy = ref enemies[j];
                    if (!enemy.IsActive) continue;

                    if (Raylib.CheckCollisionRecs(projectile.GetColliderRect(),enemy.GetColliderRect))
                    {
                        // Handle collisions
                        projectile.IsActive = false; // Deactivate the projectile
                        enemy.Health -= projectile.Damage;
                    
                        if (enemy.Health <= 0)
                        {
                            enemy.IsActive = false; // Deactivate the enemy if health is zero
                        }
                    
                    }
                }
            }
            
            if (projectile.ProjectileSource == ProjectileSource.Enemy || 
                projectile.ProjectileSource == ProjectileSource.Environment ||
                projectile.ProjectileSource == ProjectileSource.All)
            {
                // Check collision with player
                
            }
           
            
            
            
            // Check collision with environment
            if (projectile.ProjectileSource == ProjectileSource.Player ||
                projectile.ProjectileSource == ProjectileSource.Enemy ||
                projectile.ProjectileSource == ProjectileSource.All)
            {
                //TODO: Implement environment collision logic
            }
        }
        
        // check enemy collisions with player
        // Check collision with each enemy
        var playerCollider = player.GetColliderRect();
        for (int i = 0; i < enemies.Length;i++)
        {
            ref Enemy enemy = ref enemies[i];
            if (!enemy.IsActive) continue;

            if (Raylib.CheckCollisionRecs(enemy.GetColliderRect, playerCollider))
            {
                player.TakeDamage(100); //kill player for now
            }
        }
    }
}

