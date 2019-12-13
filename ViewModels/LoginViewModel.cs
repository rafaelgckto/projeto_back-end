using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToBuy.ViewModels {
    public class LoginViewModel {
        [Required(ErrorMessage = "Informe o e-mail")]
        [Column ("emailUsuario")]
        [StringLength (50)]
        public string EmailUsuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [Column ("senhaUsuario")]
        [StringLength (300)]
        public string SenhaUsuario { get; set; }
    }
}