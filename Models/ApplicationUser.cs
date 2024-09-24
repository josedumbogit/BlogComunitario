using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogComunitario.Models
{
    public class ApplicationUser: IdentityUser
    {
        // Nome completo do usuário
        [PersonalData]
        [MaxLength(100)]
        public string FullName { get; set; }

        // Gênero do usuário
        [PersonalData]
        public string Gender { get; set; }

        // Data de nascimento do usuário
        [PersonalData]
        public DateTime? DateOfBirth { get; set; }

        // URL da foto de perfil
        [PersonalData]
        [MaxLength(255)]
        public string ProfilePictureUrl { get; set; }

        // Biografia do usuário
        [PersonalData]
        [MaxLength(500)]
        public string Bio { get; set; }

        // Data de criação da conta (não marcada como PersonalData)
        public DateTime AccountCreationDate { get; set; } = DateTime.UtcNow;

        // Status da conta (ativo ou inativo)
        public bool IsActive { get; set; } = true;
    }
}
