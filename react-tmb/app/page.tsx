// const user = {
//   name: 'Hedy Lamarr',
//   imageUrl: 'https://i.imgur.com/yXOvdOSs.jpg',
//   imageSize: 90,
// };

// export default function Home() {
//   return (

//      <>
//       <h1>{user.name}</h1>
//       <img
//         className="avatar"
//         src={user.imageUrl}
//         alt={'Photo of ' + user.name}
//         style={{
//           width: user.imageSize,
//           height: user.imageSize
//         }}
//       />
//     </>

//   );
// }

// Adicionando diretiva 
'use client'

import { useState } from 'react';
import { OrderForm } from './pages/OrderForm';
import { OrderList } from './pages/OrderList';
import { SearchBar } from './pages/SearchBar';
import React, { useEffect } from 'react';


export interface Order {
  id: string;
  cliente: string;
  produto: string;
  valor: number;
  createdAt: Date;
  status: number;
  // status: ['pending' , 'processing' , 'shipped' , 'delivered' , 'cancelled'];
  // status: 'pending' | 'processing' | 'shipped' | 'delivered' | 'cancelled';
  // quantity: number;
}

const statusStrings = ['pending', 'processing', 'shipped', 'delivered', 'cancelled'];

function App() {

  const [orders, setOrders] = useState<Order[]>([]);

  useEffect(() => {
    fetch('https://localhost:44392/orders')
      .then(response => response.json())
      .then(data => {
        console.log(data);
        setOrders(data);
      })
      .catch(error => {
        console.log('Error:', error);  
      });
  }, []);
  // const [orders, setOrders] = useState<Order[]>([
  //   {
  //     id: 'ORD-001',
  //     customerName: 'João Silva',
  //     product: 'Notebook Dell',
  //     quantity: 1,
  //     status: 'delivered',
  //     createdAt: new Date('2026-02-01'),
  //     total: 3500.00
  //   },
  //   {
  //     id: 'ORD-002',
  //     cliente: 'Maria Santos',
  //     produto: 'Mouse Logitech',
  //     quantity: 2,
  //     status: 'shipped',
  //     createdAt: new Date('2026-02-05'),
  //     total: 180.00
  //   },
  //   {
  //     id: 'ORD-003',
  //     customerName: 'Carlos Oliveira',
  //     product: 'Teclado Mecânico',
  //     quantity: 1,
  //     status: 'processing',
  //     createdAt: new Date('2026-02-07'),
  //     total: 450.00
  //   },
  // ]);

  
  
  const [searchTerm, setSearchTerm] = useState('');

  const handleCreateOrder = (orderData: Omit<Order, 'id' | 'createdAt'>) => {
    const newOrder: Order = {
      ...orderData,
      id: `ORD-${String(orders.length + 1).padStart(3, '0')}`,
      createdAt: new Date(),
    };
    setOrders([newOrder, ...orders]);
  };

  const handleUpdateStatus = (orderId: string, newStatus: Order['status']) => {
    setOrders(orders.map(order => 
      order.id === orderId ? { ...order, status: newStatus } : order
    ));
  };

  const filteredOrders = orders.filter(order => 
    order.id ||
    order.cliente.toLowerCase().includes(searchTerm.toLowerCase()) ||
    order.produto.toLowerCase().includes(searchTerm.toLowerCase()) ||
    statusStrings[order.status].includes(searchTerm.toLowerCase())
  );

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6 sm:py-8">
        <div className="mb-6 sm:mb-8">
          <h1 className="text-3xl sm:text-4xl mb-2">Sistema de Pedidos</h1>
          <p className="text-gray-600">Crie, pesquise e acompanhe seus pedidos</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-6">
          <div className="lg:col-span-1">
            <OrderForm onSubmit={handleCreateOrder} />
          </div>
          
          <div className="lg:col-span-2">
            <div className="bg-white rounded-lg shadow-sm p-4 sm:p-6">
              <div className="mb-4 sm:mb-6">
                <SearchBar 
                  value={searchTerm} 
                  onChange={setSearchTerm}
                  resultsCount={filteredOrders.length}
                  totalCount={orders.length}
                />
              </div>
              
              <OrderList 
                orders={filteredOrders} 
                onUpdateStatus={handleUpdateStatus}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
