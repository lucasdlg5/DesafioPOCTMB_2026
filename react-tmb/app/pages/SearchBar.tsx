import { Search } from 'lucide-react';

interface SearchBarProps {
  value: string;
  onChange: (value: string) => void;
  resultsCount: number;
  totalCount: number;
}

export function SearchBar({ value, onChange, resultsCount, totalCount }: SearchBarProps) {
  return (
    <div>
      <div className="relative">
        <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" />
        <input
          type="text"
          value={value}
          onChange={(e) => onChange(e.target.value)}
          placeholder="Buscar por ID, cliente, produto ou status..."
          className="w-full pl-10 pr-4 py-2.5 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>
      {value && (
        <p className="text-sm text-gray-600 mt-2">
          Mostrando {resultsCount} de {totalCount} pedidos
        </p>
      )}
    </div>
  );
}
