import ProductService from '@/common/services/product';
import React from 'react';
import { X } from 'lucide-react';
import { toast } from 'sonner';

type ProductDeleteModalProps = {
  phrase: string;
  btnConfirmLabel: string;
  btnCancelLabel: string;
  productId: string;
  isOpen: boolean;
  onConfirm: () => void;
  onClose: () => void;
};

const ProductDeleteModal: React.FC<ProductDeleteModalProps> = ({
  phrase,
  btnConfirmLabel,
  btnCancelLabel,
  productId,
  isOpen,
  onConfirm,
  onClose,
}) => {
  const handleConfirm = async () => {
    try {
      await ProductService.DeleteProduct(productId);
      onConfirm();
    } catch (error) {
      toast.error('Erro ao excluir matéria prima.');
    }
  };

  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none bg-black/50"
      onMouseDown={(e) => {
        if (e.target === e.currentTarget) {
          onClose();
        }
      }}
    >
      <div className="relative w-full max-w-md mx-auto my-6 px-4">
        <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
          {/* Header */}
          <div className="flex items-start justify-between p-5 border-b border-solid border-gray-200 rounded-t">
            <h3 className="text-xl font-semibold text-red-600">
              Atenção
            </h3>
            <button
              className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
              onClick={onClose}
            >
              <X className="w-6 h-6 text-gray-500 hover:text-gray-700" />
            </button>
          </div>
          
          {/* Body */}
          <div className="relative p-6 flex-auto">
            <p className="my-4 text-gray-600 text-lg leading-relaxed">
              {phrase}
            </p>
          </div>

          {/* Footer */}
          <div className="flex items-center justify-end p-6 border-t border-solid border-gray-200 rounded-b gap-2">
            <button
              className="text-gray-500 background-transparent font-bold uppercase px-6 py-2 text-sm outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:text-gray-700"
              type="button"
              onClick={onClose}
            >
              {btnCancelLabel}
            </button>
            <button
              className="bg-red-500 text-white active:bg-red-600 font-bold uppercase text-sm px-6 py-3 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1 mb-1 ease-linear transition-all duration-150 hover:bg-red-600"
              type="button"
              onClick={handleConfirm}
            >
              {btnConfirmLabel}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductDeleteModal;
