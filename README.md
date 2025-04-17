# FastReportAPI

Projeto desenvolvido com o objetivo de gerar relatórios em formato PDF a partir de dados armazenados no **SQL Server**, utilizando o **FastReport.Net** integrado a uma API em **.NET 9**.

A API implementa dois modos distintos de acesso a dados:

- Através do **Entity Framework Core**, utilizando o padrão **Repositório**
- Utilizando **ADO.NET puro**, com `DataTable`, para cenários que exigem maior controle e desempenho

## 🧰 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [FastReport.Net](https://www.fast-report.com/en/product/fast-report-net/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/)
- Entity Framework Core
- ADO.NET (uso de `SqlConnection`, `SqlCommand`, `DataTable`, etc.)
- Padrão Repositório

## 📂 Estrutura do Projeto

![image](https://github.com/user-attachments/assets/367fb931-e5c3-48fa-87ca-dc3c0639390b)


## ✅ Funcionalidades

- Geração de relatórios em PDF a partir de dados do banco
- Escolha entre leitura via Entity Framework ou ADO.NET
- Exportação de relatórios com nome baseado na data/hora
- Retorno do relatório diretamente como `FileStreamResult`

### 🖼️ Exemplo de Endpoint

```csharp
[HttpGet("GetProductsReport")]
public async Task<ActionResult<FileStream>> GetProductsReport()

// ADO.NET com DataTable
// var ms = _productsServices.GetProductsMemoryStreamDataTable();

// Entity Framework com IEnumerable
var ms = await _productsServices.GetProductsMemoryStreamIEnumerable();
