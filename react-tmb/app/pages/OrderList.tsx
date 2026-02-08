import { Package, Clock, Truck, CheckCircle, XCircle } from 'lucide-react';
import type { Order } from '../page';

interface OrderListProps {
  orders: Order[];
  onUpdateStatus: (orderId: string, newStatus: Order['status']) => void;
}

const statusConfig = {
  pending: {
    label: 'Pendente',
    icon: Clock,
    color: 'bg-yellow-100 text-yellow-800 border-yellow-200',
  },
  processing: {
    label: 'Processando',
    icon: Package,
    color: 'bg-blue-100 text-blue-800 border-blue-200',
  },
  shipped: {
    label: 'Enviado',
    icon: Truck,
    color: 'bg-purple-100 text-purple-800 border-purple-200',
  },
  delivered: {
    label: 'Entregue',
    icon: CheckCircle,
    color: 'bg-green-100 text-green-800 border-green-200',
  },
  cancelled: {
    label: 'Cancelado',
    icon: XCircle,
    color: 'bg-red-100 text-red-800 border-red-200',
  },
};

export function OrderList({ orders, onUpdateStatus }: OrderListProps) {
  if (orders.length === 0) {
    return (
      <div className="text-center py-12 text-gray-500">
        <Package className="w-12 h-12 mx-auto mb-3 opacity-50" />
        <p>Nenhum pedido encontrado</p>
      </div>
    );
  }

  return (
    <div className="space-y-3">
      <h2 className="text-xl sm:text-2xl mb-4">Todos os Pedidos</h2>
      
      {orders.map((order) => {
        const StatusIcon = Clock;
        
        return (
          <div
            key={order.id}
            className="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow"
          >
            <div className="flex flex-col sm:flex-row sm:items-start sm:justify-between gap-3">
              <div className="flex-1 min-w-0">
                <div className="flex items-start gap-3 mb-2">
                  <div className="flex-1">
                    <p className="font-mono text-sm text-gray-500">{order.id}</p>
                    <p className="mt-1 truncate">{order.cliente}</p>
                  </div>
                  <div
                    className={`flex items-center gap-1.5 px-2.5 py-1 rounded-full border text-sm whitespace-nowrap ${
                      Clock
                    }`}
                  >
                    <StatusIcon className="w-4 h-4" />
                    {/* <span className="hidden sm:inline">{statusConfig[order.status].label}</span> */}
                  </div>
                </div>
                
                <div className="text-sm text-gray-600 space-y-1">
                  <p className="truncate">
                    <span className="text-gray-500">Produto:</span> {order.produto}
                  </p>
                  <p>
                    <span className="text-gray-500">Quantidade:</span> {order.valor}
                  </p>
                  <p>
                    <span className="text-gray-500">Total:</span> R$ {order.valor.toFixed(2)}
                  </p>
                  <p className="text-xs text-gray-400">
                    {/* {order.createdAt.toLocaleDateString('pt-BR')} */}
                  </p>
                </div>
              </div>
              
              <div className="flex sm:flex-col gap-2">
                <select
                  value={order.status}
                  onChange={(e) => onUpdateStatus(order.id, e.target.value as Order['status'])}
                  className="px-3 py-1.5 border border-gray-300 rounded-md text-sm focus:outline-none focus:ring-2 focus:ring-blue-500 bg-white"
                >
                  <option value="pending">Pendente</option>
                  <option value="processing">Processando</option>
                  <option value="shipped">Enviado</option>
                  <option value="delivered">Entregue</option>
                  <option value="cancelled">Cancelado</option>
                </select>
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );
}
