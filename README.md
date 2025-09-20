# Eventify
Software de gestão de eventos

# Para rodar Migrations

Clicar com o botão direito no projeto "Infrastructure" e "Definir como projeto de inicialização"

Ir na barra de pesquisa do Visual Studio e pesquisar:
Package Manager Console

Ao abrir, rodar o codigo a seguir pra adicionar a migration:
Add-Migration <nome_que_quiser_pra_funcionalidade> -StartupProject Eventify

Depois rodar:
Update-Database -StartupProject Eventify
