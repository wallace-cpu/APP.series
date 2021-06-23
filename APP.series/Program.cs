using DIO.Series;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.series
{
	class Program
	{
		static SerieRepositorio repositorio = new SerieRepositorio();
		static void Main(string[] args)
		{
			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos servi�os.");
			Console.ReadLine();
		}

		private static void ExcluirSerie()
		{
			Console.Write("Digite o id da s�rie: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceSerie);
		}

		private static void VisualizarSerie()
		{
			Console.Write("Digite o id da s�rie: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

		private static void AtualizarSerie()
		{
			Console.Write("Digite o id da s�rie: ");
			int indiceSerie = int.Parse(Console.ReadLine());

			
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o g�nero entre as op��es acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o T�tulo da S�rie: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de In�cio da S�rie: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descri��o da S�rie: ");
			string entradaDescricao = Console.ReadLine();

			Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
		}
		private static void ListarSeries()
		{
			Console.WriteLine("Listar s�ries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma s�rie cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
				var excluido = serie.retornaExcluido();

				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Exclu�do*" : ""));
			}
		}

		private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova s�rie");

			
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.Write("Digite o g�nero entre as op��es acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o T�tulo da S�rie: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de In�cio da S�rie: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descri��o da S�rie: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaSerie);
		}

		private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO S�ries a seu dispor!!!");
			Console.WriteLine("Informe a op��o desejada:");

			Console.WriteLine("1- Listar s�ries");
			Console.WriteLine("2- Inserir nova s�rie");
			Console.WriteLine("3- Atualizar s�rie");
			Console.WriteLine("4- Excluir s�rie");
			Console.WriteLine("5- Visualizar s�rie");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
	}
}
