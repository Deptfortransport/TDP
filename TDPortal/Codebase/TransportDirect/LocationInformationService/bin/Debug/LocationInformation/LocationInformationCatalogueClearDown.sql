USE TransientPortal


DELETE FROM LocationInformation
WHERE (ExternalLinkID IN ('{E7A4CBCA-9833-MITE49b5-8929-EED3C9DF864B}', '{E7A4CBCA-9833-MITE49b5-8929-EED3C9DF864C}',
			'{E345D45E-A41E-MITE4eb3-8A98-DB092C9A3C76}', '{E345D45E-A41E-MITE4eb3-8A98-DB092C9A3C77}', 
			'{91D98442-BB05-MITE46de-8DA7-F5EB345A91B9}', '{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BE}',
			'{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BC}', '{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BD}'))


DELETE FROM LocationInformation
WHERE (Naptan IN ('9200ABC', '9200DEF', '9200GHI', '9200JKL'))


DELETE FROM ExternalLinks
WHERE     ([ID] IN ('{E7A4CBCA-9833-MITE49b5-8929-EED3C9DF864B}', '{E7A4CBCA-9833-MITE49b5-8929-EED3C9DF864C}',
			'{E345D45E-A41E-MITE4eb3-8A98-DB092C9A3C76}', '{E345D45E-A41E-MITE4eb3-8A98-DB092C9A3C77}', 
			'{91D98442-BB05-MITE46de-8DA7-F5EB345A91B9}', '{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BE}',
			'{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BC}', '{0B543C61-8E27-MITE46a1-8B14-4ED76D76F5BD}'))
