﻿using LanchesMC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMC.Repositories
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
