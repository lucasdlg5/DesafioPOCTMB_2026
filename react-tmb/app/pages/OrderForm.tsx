import { useState } from 'react';
import { Plus } from 'lucide-react';
import type { Order } from '../page';

interface OrderFormProps {
  onSubmit: (order: Omit<Order, 'id' | 'createdAt'>) => void;
}

export function OrderForm({ onSubmit }: OrderFormProps) {
  const [customerName, setCustomerName] = useState('');
  const [product, setProduct] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [total, setTotal] = useState(0);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!customerName || !product || quantity <= 0 || total <= 0) {
      alert('Por favor, preencha todos os campos corretamente');
      return;
    }

    onSubmit({
      customerName,
      product,
      quantity,
      total,
      status: 'pending',
    });

    // Reset form
    setCustomerName('');
    setProduct('');
    setQuantity(1);
    setTotal(0);
  };

  return (
    <div className="bg-white rounded-lg shadow-sm p-4 sm:p-6">
      <h2 className="text-xl sm:text-2xl mb-4 sm:mb-6">Novo Pedido</h2>
      
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label htmlFor="customerName" className="block text-sm mb-1 text-gray-700">
            Nome do Cliente
          </label>
          <input
            type="text"
            id="customerName"
            value={customerName}
            onChange={(e) => setCustomerName(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Digite o nome do cliente"
            required
          />
        </div>

        <div>
          <label htmlFor="product" className="block text-sm mb-1 text-gray-700">
            Produto
          </label>
          <input
            type="text"
            id="product"
            value={product}
            onChange={(e) => setProduct(e.target.value)}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Digite o nome do produto"
            required
          />
        </div>

        <div>
          <label htmlFor="quantity" className="block text-sm mb-1 text-gray-700">
            Quantidade
          </label>
          <input
            type="number"
            id="quantity"
            min="1"
            value={quantity}
            onChange={(e) => setQuantity(parseInt(e.target.value))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            required
          />
        </div>

        <div>
          <label htmlFor="total" className="block text-sm mb-1 text-gray-700">
            Valor Total (R$)
          </label>
          <input
            type="number"
            id="total"
            min="0.01"
            step="0.01"
            value={total}
            onChange={(e) => setTotal(parseFloat(e.target.value))}
            className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="0.00"
            required
          />
        </div>

        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2.5 px-4 rounded-md hover:bg-blue-700 transition-colors flex items-center justify-center gap-2"
        >
          { <Plus className="w-5 h-5" /> }
          Criar Pedido
        </button>
      </form>
    </div>
  );
}
