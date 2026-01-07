'use client';

import React from 'react';
import { AlertCircle, X } from 'lucide-react';
import { toast } from 'sonner';
import GroupService from '@/common/services/group';

type GroupDeleteModalProps = {
  phrase: string;
  btnConfirmLabel: string;
  btnCancelLabel: string;
  groupId: string;
  isOpen: boolean;
  onConfirm: () => void;
  onClose: () => void;
};

const GroupDeleteModal: React.FC<GroupDeleteModalProps> = ({
  phrase,
  btnConfirmLabel,
  btnCancelLabel,
  groupId,
  isOpen,
  onConfirm,
  onClose,
}) => {
  const handleConfirm = async () => {
    try {
      await GroupService.DeleteGroup(groupId);
      onConfirm();
    } catch (err: any) {
      const errorMessage =
        err?.response?.data?.detail || err?.response?.data?.message || err?.message || 'Erro ao deletar grupo';
      toast.error(errorMessage);
      onClose();
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
      <div className="relative w-full max-w-sm mx-auto my-6 px-4">
        <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
          {/* Header */}
          <div className="flex items-start justify-between p-5 border-b border-solid border-gray-200 rounded-t">
            <div className="flex items-center gap-3">
              <AlertCircle className="w-6 h-6 text-red-500" />
              <h3 className="text-xl font-semibold">Confirmar Exclusão</h3>
            </div>
            <button
              className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
              onClick={onClose}
            >
              <X className="w-6 h-6 text-gray-500 hover:text-gray-700" />
            </button>
          </div>

          {/* Body */}
          <div className="relative p-6 flex-auto">
            <p className="text-gray-700 mb-6">{phrase}</p>
            <div className="flex gap-3 justify-end">
              <button
                className="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400 transition-colors font-semibold"
                onClick={onClose}
              >
                {btnCancelLabel}
              </button>
              <button
                className="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600 transition-colors font-semibold"
                onClick={handleConfirm}
              >
                {btnConfirmLabel}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GroupDeleteModal;
