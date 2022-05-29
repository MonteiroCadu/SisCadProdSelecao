using Application.Dtos;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public  class SisCadProdutosProfile : Profile
    {
        public SisCadProdutosProfile()
        {
            //o uso do ReverseMap() e o mesmo que mapear tbm no sentido contrario
            //CreateMap<EventoDto,Evento>() poise precisamos mapear as duas vias
            //de Evento para EventoDto e de EventoDto para Evento 

            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            
        }
    }
}
