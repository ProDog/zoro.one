1.bitcoin ���õĿͻ���

https://bitcoin.org/en/download

bitcoin core ��Ŀǰ��õĿͻ���

��ֻ���������windows �汾

bitcoinֱ���ṩ ��ʽ�� �� testnet ��������

C:\Program Files\Bitcoin>bitcoin-qt ����
C:\Program Files\Bitcoin>bitcoin-qt -testnet ����������

bitcoin �ͻ����ṩ��rpc����

ʹ�����������
C:\Program Files\Bitcoin>bitcoin-qt -server -rest -testnet -rpcuser=1 -rpcpassword=1
-server �Ǵ򿪷���������ѡ
-rest ������������ ������������ȡ������Ϣ̫�٣������Լ������磬���Բ���
-testnet ��ʾ�򿪲������磬��ʱĬ��rpc�˿�Ϊ18332 �������������磬��Ĭ��rpc�˿�Ϊ8332
-rpcuser ����rpc�����û��� ��ѡ
-rpcpassword ����rpc�������� ��ѡ


2.���Գ���˵��

nuget ��װNBitcoin ֧��netcore

��RestClient�����ܲ����Լ��ӣ�����

ʹ��RPCClinet
			
			var key = new System.Net.NetworkCredential("1", "1");
            
			var uri = new Uri("http://127.0.0.1:18332");
            
			NBitcoin.RPC.RPCClient rpcC = new NBitcoin.RPC.RPCClient(key, uri);

�õ�������Ϣ������߶ȣ���ʾ��

��ʹ�����������õ�һ�����������еĽ��ף���ʾ����

���ر������Ͻ��ױȽϵ�������dump����ʾ����

utxoģ��˵����

��μ��һ��ת�뽻�ף������׵�output ������û�м��ӵĵ�ַ����