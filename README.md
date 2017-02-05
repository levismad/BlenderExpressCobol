# BlenderExpressCobol
## Problema:
Era uma vez um projeto com cronograma apertado ...
> (Vou colocar na minha "Aqui jaz um projeto com cronograma apertado...")

... no qual era necessário a criação de dois serviços - wcf. Um deveria consumir uma chamada Http de um servidor "Cobol - Microfocus".E outro que recebe determinadas informações , aplica regras de validação (Nada muito além de 25) e para então salvar em um banco de dados Oracle.

## Solução

Criação de um metódo estático que realiza a criação da entrada COBOL a partir de um objeto por meio do uso de `CustomAttributes`.
Criação de um metódo estático para a deserializar o resultado retornado pelo COBOL em um objeto genérico `<T>`.
Criação de um Repositório de regras para aplicar todas as 25 regras de forma sequencial contra os dados de entrada.

## Why Blender again?

Blender works at Planet Express, and even that doesn't seems like, he do all the heavy job (these bottles ain't gonna empty themselves).
Just kidding, the real name of the application is: `quick_brown_wcf_jumps_over_lazy_cobol` but was too long and i will stick to the name pattern BlenderExpress*SomethingElse*.

## But why Express?

Hey i am just a one developer army, the solution maybe not entirely awesome but will do the work painless way.
